using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
    List<Neuron>[] layers;
    public int layerCount;
    public int layerSize;
    public int IntervalX;
    public int IntervalY;
    public GameObject neuronPrefab;
    public TMP_InputField layerCountTextField;
    public TMP_InputField layerSizeTextField;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
   
    }

    public List<Neuron> GetNeuronsByLayerIndex(int layerIndex)
    {
        return layers[layerIndex];
    }

    public void ClickSetButton()
    {
        string layerCountText = layerCountTextField.text;
        string layerSizeText = layerSizeTextField.text;
        if (CatchIntFormatError(layerCountText) || CatchIntFormatError(layerSizeText))
        {
            return;
        }
        
        layerCount = int.Parse(layerCountText);
        layerSize = int.Parse(layerSizeText);   

        Neuron[] preNeurons = FindObjectsOfType<Neuron>();
        if (preNeurons != null)
        {
            for (int i = 0; i < preNeurons.Length; i++)
            {
                Destroy(preNeurons[i].gameObject);
            }
        }

        layers = new List<Neuron>[layerCount];
        for (int i = 0; i < layerCount; i++)
        {
            layers[i] = new List<Neuron>();
            for (int j = 0; j < layerSize; j++)
            {
                Neuron neuron = Instantiate(neuronPrefab).GetComponent<Neuron>();
                neuron.transform.position = new Vector2(-((IntervalX * (layerCount - 1)) / 2) + IntervalX * i,
                                                        -((IntervalY * (layerSize - 1)) / 2) + IntervalY * j);
                layers[i].Add(neuron);
                
            }
        }
    }

    bool CatchIntFormatError(string s)
    {
        if (s.Length == 0)
        {
            return true;
        }

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] < '0' || s[i] > '9')
            {
                return true;
            }
        }
        return false;
    }

}
