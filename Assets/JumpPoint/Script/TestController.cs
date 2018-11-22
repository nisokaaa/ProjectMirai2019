// https://www.urablog.xyz/entry/2017/05/16/235548
using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField]
    private Transform m_shootPoint = null;
    [SerializeField]
    private Transform m_target = null;
    [SerializeField]
    private GameObject m_shootObject = null;

    [SerializeField]
    float angle = 60.0f;

    [SerializeField]
    float time = 2.0f;

    [SerializeField]
    float height = 3.0f;

    [SerializeField]
    float speed = 3.0f;
    
    enum SHUT_TYPE
    {
        SHUT_TYPE_ANGLE = 0,
        SHUT_TYPE_TIME,
        SHUT_TYPE_HEIGHT,
        SHUT_TYPE_SPEED,
        SHUT_TYPE_MAX
    };

    [SerializeField]
    SHUT_TYPE shutType;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_target != null)
        {
            switch(shutType)
            {
                case SHUT_TYPE.SHUT_TYPE_ANGLE:
                    ShootAngle(m_target.position);
                    break;
                case SHUT_TYPE.SHUT_TYPE_TIME:
                    ShootTime(m_target.position);
                    break;
                case SHUT_TYPE.SHUT_TYPE_HEIGHT:
                    ShootHeight(m_target.position);
                    break;
                case SHUT_TYPE.SHUT_TYPE_SPEED:
                    ShootSpeed(m_target.position);
                    break;
            }
            
        }
    }

    private void ShootHeight(Vector3 i_targetPosition)
    {
        // とりあえず適当に3mぐらいで高さまで飛ぶよ！
        ShootFixedHeight(i_targetPosition, height);
    }
    private void ShootAngle(Vector3 i_targetPosition)
    {
        // とりあえず適当に60度でかっ飛ばすとするよ！
        ShootFixedAngle(i_targetPosition, angle);
    }

    private void ShootTime(Vector3 i_targetPosition)
    {
        // とりあえず適当に2秒ぐらいで到着するようにするよ！
        ShootFixedTime(i_targetPosition, time);
    }
    private void ShootSpeed(Vector3 i_targetPosition)
    {
        // とりあえず適当に3m/sぐらいで高さまで飛ぶよ！
        ShootFixedSpeedInPlaneDirection(i_targetPosition, speed);
    }

    private void ShootFixedSpeedInPlaneDirection(Vector3 i_targetPosition, float i_speed)
    {
        if (i_speed <= 0.0f)
        {
            // その位置に着地させることは不可能のようだ！
            Debug.LogWarning("!!");
            return;
        }

        // xz平面の距離を計算。
        Vector2 startPos = new Vector2(m_shootPoint.transform.position.x, m_shootPoint.transform.position.z);
        Vector2 targetPos = new Vector2(i_targetPosition.x, i_targetPosition.z);
        float distance = Vector2.Distance(targetPos, startPos);

        float time = distance / i_speed;

        ShootFixedTime(i_targetPosition, time);
    }

    private void ShootFixedHeight(Vector3 i_targetPosition, float i_height)
    {
        float t1 = CalculateTimeFromStartToMaxHeight(i_targetPosition, i_height);
        float t2 = CalculateTimeFromMaxHeightToEnd(i_targetPosition, i_height);

        if (t1 <= 0.0f && t2 <= 0.0f)
        {
            // その位置に着地させることは不可能のようだ！
            Debug.LogWarning("!!");
            return;
        }


        float time = t1 + t2;

        ShootFixedTime(i_targetPosition, time);
    }

    private float CalculateTimeFromStartToMaxHeight(Vector3 i_targetPosition, float i_height)
    {
        float g = Physics.gravity.y;
        float y0 = m_shootPoint.transform.position.y;

        float timeSquare = 2 * (y0 - i_height) / g;
        if (timeSquare <= 0.0f)
        {
            return 0.0f;
        }

        float time = Mathf.Sqrt(timeSquare);
        return time;
    }

    private float CalculateTimeFromMaxHeightToEnd(Vector3 i_targetPosition, float i_height)
    {
        float g = Physics.gravity.y;
        float y = i_targetPosition.y;

        float timeSquare = 2 * (y - i_height) / g;
        if (timeSquare <= 0.0f)
        {
            return 0.0f;
        }

        float time = Mathf.Sqrt(timeSquare);
        return time;
    }

    private void ShootFixedTime(Vector3 i_targetPosition, float i_time)
    {
        float speedVec = ComputeVectorFromTime(i_targetPosition, i_time);
        float angle = ComputeAngleFromTime(i_targetPosition, i_time);

        if (speedVec <= 0.0f)
        {
            // その位置に着地させることは不可能のようだ！
            Debug.LogWarning("!!");
            return;
        }

        Vector3 vec = ConvertVectorToVector3(speedVec, angle, i_targetPosition);
        InstantiateShootObject(vec);
    }

    private float ComputeVectorFromTime(Vector3 i_targetPosition, float i_time)
    {
        Vector2 vec = ComputeVectorXYFromTime(i_targetPosition, i_time);

        float v_x = vec.x;
        float v_y = vec.y;

        float v0Square = v_x * v_x + v_y * v_y;
        // 負数を平方根計算すると虚数になってしまう。
        // 虚数はfloatでは表現できない。
        // こういう場合はこれ以上の計算は打ち切ろう。
        if (v0Square <= 0.0f)
        {
            return 0.0f;
        }

        float v0 = Mathf.Sqrt(v0Square);

        return v0;
    }

    private float ComputeAngleFromTime(Vector3 i_targetPosition, float i_time)
    {
        Vector2 vec = ComputeVectorXYFromTime(i_targetPosition, i_time);

        float v_x = vec.x;
        float v_y = vec.y;

        float rad = Mathf.Atan2(v_y, v_x);
        float angle = rad * Mathf.Rad2Deg;

        return angle;
    }

    private Vector2 ComputeVectorXYFromTime(Vector3 i_targetPosition, float i_time)
    {
        // 瞬間移動はちょっと……。
        if (i_time <= 0.0f)
        {
            return Vector2.zero;
        }


        // xz平面の距離を計算。
        Vector2 startPos = new Vector2(m_shootPoint.transform.position.x, m_shootPoint.transform.position.z);
        Vector2 targetPos = new Vector2(i_targetPosition.x, i_targetPosition.z);
        float distance = Vector2.Distance(targetPos, startPos);

        float x = distance;
        // な、なぜ重力を反転せねばならないのだ...
        float g = -Physics.gravity.y;
        float y0 = m_shootPoint.transform.position.y;
        float y = i_targetPosition.y;
        float t = i_time;

        float v_x = x / t;
        float v_y = (y - y0) / t + (g * t) / 2;

        return new Vector2(v_x, v_y);
    }

    private void ShootFixedAngle(Vector3 i_targetPosition, float i_angle)
    {
        float speedVec = ComputeVectorFromAngle(i_targetPosition, i_angle);
        if (speedVec <= 0.0f)
        {
            // その位置に着地させることは不可能のようだ！
            Debug.LogWarning("!!");
            return;
        }

        Vector3 vec = ConvertVectorToVector3(speedVec, i_angle, i_targetPosition);
        InstantiateShootObject(vec);
    }

    private float ComputeVectorFromAngle(Vector3 i_targetPosition, float i_angle)
    {
        // xz平面の距離を計算。
        Vector2 startPos = new Vector2(m_shootPoint.transform.position.x, m_shootPoint.transform.position.z);
        Vector2 targetPos = new Vector2(i_targetPosition.x, i_targetPosition.z);
        float distance = Vector2.Distance(targetPos, startPos);

        float x = distance;
        float g = Physics.gravity.y;
        float y0 = m_shootPoint.transform.position.y;
        float y = i_targetPosition.y;

        // Mathf.Cos()、Mathf.Tan()に渡す値の単位はラジアンだ。角度のまま渡してはいけないぞ！
        float rad = i_angle * Mathf.Deg2Rad;

        float cos = Mathf.Cos(rad);
        float tan = Mathf.Tan(rad);

        float v0Square = g * x * x / (2 * cos * cos * (y - y0 - x * tan));

        // 負数を平方根計算すると虚数になってしまう。
        // 虚数はfloatでは表現できない。
        // こういう場合はこれ以上の計算は打ち切ろう。
        if (v0Square <= 0.0f)
        {
            return 0.0f;
        }

        float v0 = Mathf.Sqrt(v0Square);
        return v0;
    }

    private Vector3 ConvertVectorToVector3(float i_v0, float i_angle, Vector3 i_targetPosition)
    {
        Vector3 startPos = m_shootPoint.transform.position;
        Vector3 targetPos = i_targetPosition;
        startPos.y = 0.0f;
        targetPos.y = 0.0f;

        Vector3 dir = (targetPos - startPos).normalized;
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
        Vector3 vec = i_v0 * Vector3.right;

        vec = yawRot * Quaternion.AngleAxis(i_angle, Vector3.forward) * vec;

        return vec;
    }

    private void InstantiateShootObject(Vector3 i_shootVector)
    {
        if (m_shootObject == null)
        {
            throw new System.NullReferenceException("m_shootObject");
        }

        if (m_shootPoint == null)
        {
            throw new System.NullReferenceException("m_shootPoint");
        }

        var obj = Instantiate<GameObject>(m_shootObject, m_shootPoint.position, Quaternion.identity);
        var rigidbody = obj.AddComponent<Rigidbody>();

        // 速さベクトルのままAddForce()を渡してはいけないぞ。力(速さ×重さ)に変換するんだ
        Vector3 force = i_shootVector * rigidbody.mass;

        rigidbody.AddForce(force, ForceMode.Impulse);
    }
} // class TestController