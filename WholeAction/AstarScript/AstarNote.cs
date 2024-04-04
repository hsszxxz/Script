namespace ns
{
    public enum NoteType
    {
        walk,
        stop,
        initiate,
        luban
    }
    ///<summary>
    ///
    ///<summary>
    public class AstarNote
    {
        public int x;
        public int y;

        public NoteType type;

        public float f, g, h;

        public AstarNote father;

        public AstarNote(int x, int y , NoteType type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
    }
}

