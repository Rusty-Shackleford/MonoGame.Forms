﻿using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace MonoGame.Forms.Anchoring
{
    public class AnchorComponent
    {
        public AnchorComponent(IAnchorable target, IAnchorable owner, AnchorType anchorType, PositionType positionType, Size offset)
        {
            _target = target;
            _owner = owner;
            _anchorType = anchorType;
            _positionType = positionType;
            _anchorOffset = offset;
            _target.OnPositionChanged += PositionChange;
            _target.OnDimmensionChanged += DimmensionChange;
            HaulAnchor();
        }

        private IAnchorable _owner;
        private IAnchorable _target;

        private PositionType _positionType;
        public PositionType PositionType
        {
            get { return _positionType; }
        }

        private AnchorType _anchorType;
        public AnchorType AnchorType
        {
            get { return _anchorType; }
        }

        private Size _anchorOffset;
        public Size AnchorOffset
        {
            get { return _anchorOffset; }
        }

        private Rectangle _targetRectangle
        {
            get
            {
                return _target.Bounds;

                //switch (_anchorType)
                //{
                //    case AnchorType.Bounds:
                //        return _target.Bounds;
                //    case AnchorType.VirtualBounds:
                //        return _target.VirtualBounds;
                //    default:
                //        return _target.Bounds;
                //}
            }
        }

        public Vector2 AnchoredPosition()
        {
            return Anchoring.GetPosition(_targetRectangle, _owner.Bounds, _positionType, _anchorOffset);
        }

        private void PositionChange(object sender, PositionChangedArgs e)
        {
            HaulAnchor();
        }
        private void DimmensionChange(object sender, EventArgs e)
        {
            HaulAnchor();
        }

        public void HaulAnchor()
        {
            var oldPos = _owner.Position;
            var newPos = AnchoredPosition();
            _owner.MoveTo(newPos);
            _owner.DistanceMoved = newPos - oldPos;
        }

        public void RemoveAnchor()
        {
            _target.OnPositionChanged -= PositionChange;
        }



    }
}
