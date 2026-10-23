using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public class CameraComponent : ComponentBase
    {
        public override ECompType ComponentType => ECompType.Camera;

        public Transform cameraParent;
        public Camera camera;
        public Vector3 initPos;
        public float initSize;
        public float orthographicSize = 12f;
        public Vector3 pos = Vector3.zero;
        public Vector3 angle = Vector3.zero;

        public CameraComponent(Entity _entity) : base(_entity)
        {
        }

        public void SetCamera(Transform _parent, Camera _camera)
        {
            this.cameraParent = _parent;
            this.camera = _camera;
            ResetParentPos();
            this.initPos = Vector3.zero;
            this.initSize = this.camera.orthographicSize;
            SetPos(this.pos);
            SetOrthographicSize(this.orthographicSize);
            SetAngle(this.angle);
        }

        public void SetPos(Vector3 _pos)
        {
            this.pos = _pos;
        }

        public void ResetParentPos()
        {
            this.pos = Vector3.zero;
        }

        public void SetOrthographicSize(float _size)
        {
            this.orthographicSize = _size;
            if (this.camera != null)
            {
                this.camera.orthographicSize = this.orthographicSize;
            }
        }

        public void SetAngle(Vector3 _angle)
        {
            this.angle = _angle;
        }

        public override void OnRecover()
        {
            cameraParent = null;
            camera = null;
        }

        protected override string GetString()
        {
            return $"cameraParent:{cameraParent}, camera:{camera},orthographicSize:{orthographicSize},angle:{this.angle},pos:{this.pos}";
        }
    }
}
