using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Golf
{
    public class Traektoria : MonoBehaviour
    {
        private LineRenderer _line;
        public GameObject Target;
        public Transform FirePointPos;
        public Rigidbody2D Rigit;
        private int steps = 50;
        private Vector3[] points;
        private Vector3 direction
        {
            get { return (Vector2)Target.transform.position - (Vector2)transform.position; }
        }
        //CharacterMotor _motor;
        private void Start()
        {
            //_motor = GetComponent<CharacterMotor>();
            _line = GetComponent<LineRenderer>();
            _line.positionCount = steps;
        }
        void Update()
        {
            FirePointPos.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.6f, gameObject.transform.position.z);
            CameraToWorldRay();
            points = Plot(Rigit, FirePointPos.position, direction * 25, steps);
            GetPos(points);
            RayCheckCollisions(points);
            SetCharacterDirection();
        }
        //ѕросчитывам массив точек
        public Vector3[] Plot(Rigidbody2D rigidbody, Vector3 pos, Vector3 velocity, int steps)
        {
            Vector3[] results = new Vector3[steps];
            float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
            Vector3 gravityAccel = Physics2D.gravity * (rigidbody.gravityScale * 250) * timestep * timestep;
            float drag = 1f - timestep * rigidbody.drag;
            Vector3 moveStep = velocity * timestep;

            for (int i = 0; i < steps; ++i)
            {
                moveStep += gravityAccel;
                moveStep *= drag;
                pos += moveStep;
                results[i] = pos;
            }

            return results;
        }
        public void GetPos(Vector3[] array)
        {
            _line.SetPositions(array);
        }
        //Ќека€ зона поражени€
        public GameObject Zone;
        //ѕускаем лучи добра из 0 точки массива дабы найти точку попадени€ луча в землю
        public void RayCheckCollisions(Vector3[] _points)
        {
            RaycastHit hit;
            for (int i = 0; i < _points.Length; i++)
            {
                if (_points[i] != points.Last())
                    if (Physics.Raycast(_points[i], _points[i + 1], out hit))
                    {
                        //≈сли попали то перемещаем зону воздействи€ в точку попадани€
                        if (hit.collider != null)
                            Zone.transform.position = _points[i];
                    }

            }
        }
        //ѕускаем луч в центральную линию (стену) игры
        private void CameraToWorldRay()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.isTrigger && hit.collider.tag == "CenterWall" && hit.collider != null)
                    Target.transform.position = hit.point;
            }
        }
        private void OnDisable()
        {
            _line.enabled = false;
            Zone.SetActive(false);
        }
        private void OnEnable()
        {
            _line.enabled = true;
            Zone.SetActive(true);
        }
        // «адаем направление персонажа
        void SetCharacterDirection()
        {
            Vector3 lhs = FirePointPos.transform.TransformDirection(Vector3.forward);
            Vector3 rhs = Target.transform.position - FirePointPos.transform.position;
            //_motor.Direction = Vector3.Dot(lhs, rhs) >= 0 ? CharacterDirection.Right : CharacterDirection.Left;
        }
    }
}
