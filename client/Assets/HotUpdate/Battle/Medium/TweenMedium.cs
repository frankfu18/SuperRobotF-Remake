using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrankGame.Battle
{
    public static class TweenMedium
    {
        /// <summary>
        /// 使用DOTween在目标Transform的局部坐标系内播放位置震动。
        /// 该入口用于存在持续父节点运动的对象，例如战斗相机：父节点继续负责跟随，子节点只叠加表现偏移。
        /// </summary>
        /// <param name="entity">持有TweenComponent的表现实体。</param>
        /// <param name="duration">震动持续时间，单位：秒。</param>
        /// <param name="localStrength">局部坐标各轴的最大震动强度；传入Z=0可避免改变相机景深。</param>
        /// <param name="vibrato">DOTween震动次数，至少为1。</param>
        /// <param name="initialLocalPosition">Tween开始和结束时必须恢复的局部坐标。</param>
        public static void Shake(Entity entity, float duration, Vector3 localStrength, int vibrato,
            Vector3 initialLocalPosition)
        {
            if (duration <= 0f || localStrength.sqrMagnitude <= 0f
                || !entity.TryGetComponent(ECompType.Tween, out TweenComponent tweenCom)
                || tweenCom.transform == null)
            {
                return;
            }

            tweenCom.StopPosTween();
            Transform target = tweenCom.transform;
            // 上一次震动可能在中途被更强档位替换；新Tween必须从稳定原点开始，不能继承旧偏移。
            target.localPosition = initialLocalPosition;
            Tweener tween = DOTween.Shake(
                () => target != null ? target.localPosition : initialLocalPosition,
                value =>
                {
                    if (target != null) target.localPosition = value;
                },
                duration,
                localStrength,
                Mathf.Max(1, vibrato),
                45f,
                true,
                ShakeRandomnessMode.Full);
            tweenCom.posTween = tween;
            tween.onComplete = () =>
            {
                if (target != null) target.localPosition = initialLocalPosition;
                // 新震动可能已经替换当前Tween；旧回调不得清空新Tween的引用。
                if (tweenCom.posTween == tween) tweenCom.posTween = null;
            };
        }

        public static void Shake(Entity _entity, float _duration, Vector3 _dir, Vector3 _initPos)
        {
            if(!_entity.TryGetComponent(ECompType.Tween, out TweenComponent tweenCom) || tweenCom.transform == null)
            {
                return;
            }

            tweenCom.StopPosTween();
            tweenCom.posTween = tweenCom.transform.DOShakePosition(_duration, _dir / 10.0f, 1, 0, false, true, ShakeRandomnessMode.Full);
            tweenCom.posTween.onComplete = () => 
            {
                tweenCom.transform.localPosition = _initPos;
                tweenCom.posTween = null;
            };
        }

        public static void Shake(Entity _entity, float _duration, Vector3 _initPos)
        {
            if (!_entity.TryGetComponent(ECompType.Tween, out TweenComponent tweenCom) || tweenCom.transform == null)
            {
                return;
            }

            tweenCom.StopPosTween();
            tweenCom.posTween = tweenCom.transform.DOShakePosition(_duration, 0.5f, 1);
            tweenCom.posTween.SetLoops(3);
            tweenCom.posTween.onComplete = () =>
            {
                tweenCom.transform.localPosition = _initPos;
                tweenCom.posTween = null;
            };
        }

        public static void Shake(Entity _entity, float _duration, float _strength, Vector3 _initPos)
        {
            if (_strength <= 0 || !_entity.TryGetComponent(ECompType.Tween, out TweenComponent tweenCom) || tweenCom.transform == null)
            {
                return;
            }

            tweenCom.StopPosTween();
            tweenCom.posTween = tweenCom.transform.DOShakePosition(_duration, _strength, 1);
            tweenCom.posTween.onComplete = () =>
            {
                tweenCom.transform.localPosition = _initPos;
                tweenCom.posTween = null;
            };
        }

        public static Tweener MoveLocalPosition(Transform _transform, Vector3 _targetPos, float _duration, Ease _ease = Ease.OutQuad, bool _ignoreTimeScale = false)
        {
            if (_transform == null)
            {
                return null;
            }

            return _transform.DOLocalMove(_targetPos, _duration).SetEase(_ease).SetUpdate(_ignoreTimeScale);
        }

        public static Tweener PlayDeathBackMove(Transform _transform, Vector3 _targetPos, float _duration, Ease _ease = Ease.OutQuad)
        {
            if (_transform == null || _duration <= 0)
            {
                return null;
            }

            _transform.DOKill();
            return _transform.DOLocalMove(_targetPos, _duration).SetEase(_ease);
        }

        public static Sequence PlayDeathPopBackMove(Transform _transform, Vector3 _firstPos, Vector3 _targetPos, float _firstDuration, float _secondDuration)
        {
            if (_transform == null)
            {
                return null;
            }

            _transform.DOKill();

            // 死亡表现分两段：先原地向上弹起，再向后位移到死亡镜头聚焦点。
            Sequence sequence = DOTween.Sequence();
            if (_firstDuration > 0)
            {
                sequence.Append(_transform.DOLocalMove(_firstPos, _firstDuration).SetEase(Ease.OutQuad));
            }
            else
            {
                _transform.localPosition = _firstPos;
            }

            if (_secondDuration > 0)
            {
                sequence.Append(_transform.DOLocalMove(_targetPos, _secondDuration).SetEase(Ease.OutQuad));
            }
            else
            {
                _transform.localPosition = _targetPos;
            }

            return sequence;
        }

        public static void StopTransformTween(Transform _transform)
        {
            if (_transform == null)
            {
                return;
            }

            _transform.DOKill();
        }

        public static Tweener MoveCameraComponentPos(CameraEntity _cameraEntity, Vector3 _targetPos, float _duration, Ease _ease = Ease.OutQuad, bool _ignoreTimeScale = false)
        {
            if (_cameraEntity == null || _cameraEntity.cameraCom == null)
            {
                return null;
            }

            Tweener tween = DOTween.To(
                () => _cameraEntity.cameraCom.pos,
                (pos) => _cameraEntity.cameraCom.SetPos(pos),
                _targetPos,
                _duration).SetEase(_ease).SetUpdate(_ignoreTimeScale);

            return tween;
        }

        public static Tweener MoveCameraOrthographicSize(CameraEntity _cameraEntity, float _targetSize, float _duration, Ease _ease = Ease.OutQuad, bool _ignoreTimeScale = false)
        {
            if (_cameraEntity == null
                || _cameraEntity.cameraCom == null
                || _cameraEntity.cameraCom.camera == null
                || !_cameraEntity.TryGetComponent(ECompType.Tween, out TweenComponent tweenCom))
            {
                return null;
            }

            tweenCom.StopSizeTween();
            tweenCom.sizeTween = _cameraEntity.cameraCom.camera.DOOrthoSize(_targetSize, _duration).SetEase(_ease).SetUpdate(_ignoreTimeScale);
            tweenCom.sizeTween.onComplete = () =>
            {
                tweenCom.sizeTween = null;
            };

            return tweenCom.sizeTween;
        }

        public static Tweener PlayParabolaLocalMove(Transform _transform, Vector3 _startPos, Vector3 _endPos, float _height, float _duration, System.Action _onComplete = null)
        {
            if (_transform == null)
            {
                return null;
            }

            _transform.localPosition = _startPos;
            Vector3 controlPos = (_startPos + _endPos) * 0.5f + Vector3.up * _height;
            Tweener tween = DOTween.To(() => 0f, (t) =>
            {
                Vector3 startToControl = Vector3.Lerp(_startPos, controlPos, t);
                Vector3 controlToEnd = Vector3.Lerp(controlPos, _endPos, t);
                _transform.localPosition = Vector3.Lerp(startToControl, controlToEnd, t);
            }, 1f, _duration);

            tween.onComplete = () =>
            {
                if (_transform != null)
                {
                    _transform.localPosition = _endPos;
                }

                _onComplete?.Invoke();
            };

            return tween;
        }

        public static void CameraMoveSlow(CameraEntity _cameraEntity)
        {
            if (!_cameraEntity.TryGetComponent(ECompType.Tween, out TweenComponent tweenCom))
            {
                return;
            }

            tweenCom.StopSizeTween();

            float lastSpeed = _cameraEntity.World.PlaySpeed;
            _cameraEntity.World.SetPlaySpeed(0.02f);
            _cameraEntity.cameraCom.camera.orthographicSize = _cameraEntity.cameraCom.initSize;

            tweenCom.sizeTween = _cameraEntity.cameraCom.camera.DOOrthoSize(10, 0.3f);
            tweenCom.sizeTween.onComplete = () =>
            {
                tweenCom.sizeTween = _cameraEntity.cameraCom.camera.DOOrthoSize(_cameraEntity.cameraCom.initSize, 0.2f).SetDelay(0.8f);
                tweenCom.sizeTween.onComplete = () =>
                {
                    _cameraEntity.World.SetPlaySpeed(lastSpeed);
                    _cameraEntity.cameraCom.camera.orthographicSize = _cameraEntity.cameraCom.initSize;
                    tweenCom.sizeTween = null;
                };
            };
        }
    }
}
