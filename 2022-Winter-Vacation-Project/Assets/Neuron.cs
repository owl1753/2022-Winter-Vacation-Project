using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour
{
    GameManager gm;
    List<float> activations;
    float[] weights;
    float bias;
    public int layerIndex;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Exp(-x));
    }
    public void Load()
    {
        float h = 0;
        for (int i = 0; i < gm.layerSize; i++)
        {
            h += weights[i] * activations[i];
        }
        h += bias;

        List<Neuron> nextNeurons = gm.GetNeuronsByLayerIndex(layerIndex + 1);
        for (int i = 0; i < nextNeurons.Count; i++)
        {
            nextNeurons[i].activations.Add(Sigmoid(h));
        }
    }
}
