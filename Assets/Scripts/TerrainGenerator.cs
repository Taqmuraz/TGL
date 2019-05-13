using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TerrainGenerator : MonoBehaviour
{
	[SerializeField] float scale = 1f;
	[SerializeField] float scaleLoss = 0.5f;
	[SerializeField] float power = 3f;
	[SerializeField] float powerLoss = 0.25f;
	[SerializeField] int resolution = 1000;
	[SerializeField] int depthCalc = 10;
	[SerializeField] int _textures = 2;
	[SerializeField] int treesCount = 25000;
	[SerializeField] TerrainData data;
	[SerializeField] Vector3 size = new Vector3 (1000f, 600f, 1000f);

	static int textures;
	static float stage;
	static float globalHeight;

	TerrainCollider coll;

	void Start ()
	{
		coll = GetComponent<TerrainCollider> ();
	}

	void MainAlgorithm ()
	{
		textures = _textures;
		float[,] heights = new float[resolution, resolution];

		globalHeight = size.y;

		float sc = scale;
		float pow = power;

		for (int i = 0; i < depthCalc; i++) {
			stage = i * (size.y / depthCalc) * 0.5f;
			GenerateRandomZone (heights, sc, pow);
			sc *= scaleLoss;
			pow *= powerLoss;
		}

		Generate (data, size, resolution, heights);

		PlaceTrees (data, treesCount);
		coll.enabled = false;
		coll.enabled = true;
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab)) {
			MainAlgorithm ();
		}
	}

	public static void PlaceTrees (TerrainData data, int count)
	{
		List<TreeInstance> trees = new List<TreeInstance> ();
		int prototypes = data.treePrototypes.Length;
		for (int i = 0; i < count; i++) {
			TreeInstance tree = new TreeInstance ();

			float rx = Random.Range (0, 1f);
			float rz = Random.Range (0, 1f);

			int rxInt = (int)(rx * data.heightmapResolution);
			int rzInt = (int)(rz * data.heightmapResolution);

			tree.position = new Vector3 (rx, data.GetHeight(rxInt, rzInt) / data.size.y, rz);
			tree.color = new Color32 (255, 255, 255, 255);
			tree.heightScale = 1f;
			tree.prototypeIndex = Random.Range (0, prototypes);
			tree.widthScale = 1f;
			trees.Add (tree);
		}
		data.treeInstances = trees.ToArray ();
	}

	public static void Generate (TerrainData data, Vector3 size, int resolution, float[,] heights)
	{
		data.size = size;
		data.heightmapResolution = resolution;
		data.SetHeights (0, 0, heights);

		data.alphamapResolution = resolution;

		float[,,] alphas = new float[data.alphamapWidth, data.alphamapHeight, textures];

		float max = 0f;

		foreach (var h in heights) {
			if (h > max) {
				max = h;
			}
		}
		max *= size.y;

		int count = alphas.GetLength (2);
		float heightPart = max / count;

		for (int i = 0; i < alphas.GetLength (0); i++) {
			for (int j = 0; j < alphas.GetLength (1); j++) {
				for (int t = 0; t < count; t++) {
					
					float delta = Mathf.Abs(heights [i, j] * size.y - heightPart * (t + 1));

					alphas [i, j, t] = 1f - delta / heightPart;
				}
			}
		}

		data.SetAlphamaps (0, 0, alphas);
	}
	public static void GenerateRandomZone (float[,] origin, float scale, float power)
	{

		Vector2 rnd = Random.insideUnitCircle;
		for (int j = 0; j < origin.GetLength(0); j++) {
			for (int i = 0; i < origin.GetLength(1); i++) {
				float f = 1f;
				if (origin[j, i] * globalHeight < stage) {
					f *= 0.5f;
				}
				//float _scale = Random.Range (scale * 0.75f, scale * 1.25f);
				float x = i * scale; // X координата вершины
				float z = j * scale; // Z координата вершины
				origin[j, i] += (Mathf.PerlinNoise (x + rnd.x, z + rnd.y)) * power * f;
			}
		}
	}
}
