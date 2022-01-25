using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour
{
    public enum Layer
    {
        InputLayer,
        HiddenLayer,
        OutputLayer
    };

    GameManager gm;
    SpriteRenderer sp;
    List<float> activations;
    float[] weights;
    float bias;
    public float a;
    public int layerIndex;
    public int preLayerSize;
    public bool endCalculation;
    public Layer layer;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        sp = GetComponent<SpriteRenderer>();
        a = 0;
        endCalculation = false;
        if (layer != Layer.InputLayer)
        {
            preLayerSize = gm.GetLayerSizeByLayerIndex(layerIndex - 1);
        }
        weights = new float[preLayerSize];
        activations = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Exp(-x));
    }
    public IEnumerator Load()
    {
        if (layer == Layer.HiddenLayer || layer == Layer.OutputLayer)
        {
            for (int i = 0; i < preLayerSize; i++)
            {
                a += weights[i] * activations[i];
            }
            a += bias;
            a = Sigmoid(a);
        }
        if (layer == Layer.InputLayer || layer == Layer.HiddenLayer)
        {
            List<Neuron> nextNeurons = gm.GetNeuronsByLayerIndex(layerIndex + 1);
            for (int i = 0; i < nextNeurons.Count; i++)
            {
                nextNeurons[i].activations.Add(a);
            }
        }
        if (layer == Layer.OutputLayer)
        {
            print(a);
        }

        sp.color = new Color(sp.color.r * (1 - a), sp.color.g, sp.color.b, sp.color.a);
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.white;

        endCalculation = true;
        gm.CheckEndCalculation(layerIndex);
    }
}
