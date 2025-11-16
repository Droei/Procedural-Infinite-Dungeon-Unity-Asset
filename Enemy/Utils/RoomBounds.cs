using UnityEngine;

public class RoomBounds
{
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    public RoomBounds(float minX, float maxX, float minZ, float maxZ)
    {
        this.minX = minX;
        this.maxX = maxX;
        this.minZ = minZ;
        this.maxZ = maxZ;
    }

    public float Width => maxX - minX;
    public float Depth => maxZ - minZ;

    public RoomBounds Shrink(float margin)
    {
        return new RoomBounds(minX + margin, maxX - margin, minZ + margin, maxZ - margin);
    }

    public Vector3 Center(float y = 0f)
    {
        float centerX = minX + Width / 2f;
        float centerZ = minZ + Depth / 2f;

        return new Vector3(centerX, y, centerZ);
    }



    public Vector3 RandomPoint(float y = 0f)
    {
        float x = RandomService.Range(minX, maxX);
        float z = RandomService.Range(minZ, maxZ);
        return new Vector3(x, y, z);
    }

    public Vector3 RandomSpawnPosition(float baseY, float margin)
    {
        RoomBounds safeBounds = margin > 0 ? Shrink(margin) : this;
        float y = baseY;
        return safeBounds.RandomPoint(y);
    }


    public override string ToString()
    {
        return $"RoomBounds(minX: {minX:F2}, maxX: {maxX:F2}, minZ: {minZ:F2}, maxZ: {maxZ:F2}, Width: {Width:F2}, Depth: {Depth:F2})";
    }

}
