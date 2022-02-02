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
    public SpriteRenderer sp;
    public float[] preActivations;
    public float[] weights;
    public float[] gradientOfWeights;
    public float gradeintOfBias;
    public float gradientOfActivation;
    public float bias;
    public float z;
    public float a;
    public int layerIndex;
    public int preLayerSize;
    public bool endForwardPass;
    public bool endBackwardPass;
    public float runningRate;
    public Layer layer;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        sp = GetComponent<SpriteRenderer>();
        a = 0;
        endForwardPass = false;
        endBackwardPass = false;
        if (layer != Layer.InputLayer)
        {
            preLayerSize = gm.GetLayerSizeByLayerIndex(layerIndex - 1);
        }
        weights = new float[preLayerSize];
        preActivations = new float[preLayerSize];
        gradientOfWeights = new float[weights.Length];

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-10.0f, 10.0f);
        }
        bias = Random.Range(-10.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    float Sigmoid(float x)
    {
        return 1 / (1 + Mathf.Exp(-x));
    }
    public void ForwardPass()
    {
        z = 0;
        if (layer == Layer.HiddenLayer || layer == Layer.OutputLayer)
        {
            for (int i = 0; i < preLayerSize; i++)
            {
                z += weights[i] * preActivations[i];
            }
            z += bias;
            a = Sigmoid(z);
        }
    }

    public void BackwardPass(bool updating)
    {
        if (layer == Layer.HiddenLayer || layer == Layer.OutputLayer)
        {
            for (int i = 0; i < gradientOfWeights.Length; i++)
            {
                gradientOfWeights[i] += (preActivations[i] * a * (1 - a) * gradientOfActivation);
            }
            gradeintOfBias += a * (1 - a) * gradientOfActivation; 
        }

        if (updating && !gm.testing)
        {
            updateWeights();
            updateBias();
        }
    }

    public void updateWeights()
    {
        
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] -= runningRate * (gradientOfWeights[i] / gm.batchSize);
            gradientOfWeights[i] = 0;
        }
    }

    public void updateBias()
    {
        bias -= runningRate * (gradeintOfBias / gm.batchSize);
        gradeintOfBias = 0;
    }
}
