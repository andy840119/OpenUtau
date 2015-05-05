﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenUtau.Core.USTx;

namespace OpenUtau.Core
{
    public abstract class ExpCommand : UCommand
    {
        public UVoicePart Part;
        public UNote Note;
        public string Key;
    }

    public class SetIntExpCommand : ExpCommand
    {
        public int NewValue, OldValue;
        public SetIntExpCommand(UVoicePart part, UNote note, string key, int newValue)
        {
            this.Part = part;
            this.Note = note;
            this.Key = key;
            this.NewValue = newValue;
            this.OldValue = (int)Note.Expressions[Key].Data;
        }
        public override string ToString() { return "Set note expression " + Key; }
        public override void Execute() { Note.Expressions[Key].Data = NewValue; }
        public override void Unexecute() { Note.Expressions[Key].Data = OldValue; }
    }

    public class DeletePitchPointCommand : ExpCommand
    {
        public int Index;
        public PitchPoint Point;
        public DeletePitchPointCommand(UVoicePart part, UNote note, int index)
        {
            this.Part = part;
            this.Note = note;
            this.Index = index;
            this.Point = Note.PitchBend.Points[Index];
        }
        public override string ToString() { return "Delete pitch point"; }
        public override void Execute() { Note.PitchBend.Points.RemoveAt(Index); }
        public override void Unexecute() { Note.PitchBend.Points.Insert(Index, Point); }
    }

    public class ChangePitchPointShapeCommand : ExpCommand
    {
        public PitchPoint Point;
        public PitchPointShape NewShape;
        public PitchPointShape OldShape;
        public ChangePitchPointShapeCommand(PitchPoint point, PitchPointShape shape)
        {
            this.Point = point;
            this.NewShape = shape;
            this.OldShape = point.Shape;
        }
        public override string ToString() { return "Change pitch point shape"; }
        public override void Execute() { Point.Shape = NewShape; }
        public override void Unexecute() { Point.Shape = OldShape; }
    }

    public class SnapPitchPointCommand : ExpCommand
    {
        bool NewSnap;
        bool OldSnap;
        public SnapPitchPointCommand(UNote note, bool snap)
        {
            this.Note = note;
            this.NewSnap = snap;
            this.OldSnap = note.PitchBend.SnapFirst;
        }
        public override string ToString() { return "Snap pitch point"; }
        public override void Execute() { Note.PitchBend.SnapFirst = NewSnap; }
        public override void Unexecute() { Note.PitchBend.SnapFirst = OldSnap; }
    }

    public class AddPitchPointCommand : ExpCommand
    {
        public int Index;
        public PitchPoint Point;
        public AddPitchPointCommand(UNote note, PitchPoint point, int index)
        {
            this.Note = note;
            this.Index = index;
            this.Point = point;
        }
        public override string ToString() { return "Add pitch point"; }
        public override void Execute() { Note.PitchBend.Points.Insert(Index, Point); }
        public override void Unexecute() { Note.PitchBend.Points.RemoveAt(Index); }
    }
}