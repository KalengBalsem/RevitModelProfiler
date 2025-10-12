namespace RevitModelProfiler.src.UI
{
    public class FamilyDataViewModel
    {
        public long FamilyId { get; set; }
        public string FamilyName { get; set; } = "";
        public int InstanceCount { get; set; }
        public int FaceCount { get; set; }
        public int MeshTriangleCount { get; set; }
        // FamilyFileSizeMB property removed
    }
}

