
namespace DTerrain
{
    public class BasicPaintableLayer : PaintableLayer<PaintableChunk>
    {
        //CHUNK SIZE X!!!!
        [NaughtyAttributes.Button()]
        public virtual void SetUp()
        {
            SpawnChunks();
            InitChunks();
        }

        

        public void Start()
        {
           SpawnChunks();
           InitChunks();
        }
    }
}
