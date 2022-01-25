using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
    struct Pair
    {
        public float[,] nums;
        public float[] answer;

    };

    Pair[] learningDatas;
    Pair[] testDatas;

    List<Neuron>[] layers;
    public int dataSize;
    public int layerCount;
    public int layerSize;
    public int IntervalX;
    public int IntervalY;
    public int epochs;
    public int count;
    public bool learning;
    public GameObject neuronPrefab;
    public TMP_InputField layerCountTextField;
    public TMP_InputField layerSizeTextField;

    // Start is called before the first frame update
    void Start()
    {
        #region Set_Data
        learningDatas = new Pair[dataSize];
        testDatas = new Pair[10];

        for (int i = 0; i < testDatas.Length; i++)
        {
            testDatas[i].nums = new float[9, 9];
            testDatas[i].answer = new float[10];

            for (int j = 0; j < 9; j++)
            {
                for (int k = 0; k < 9; k++)
                {
                    testDatas[i].nums[j, k] = 0;
                }
            }

            for (int j = 0; j < 10; j++)
            {
                testDatas[i].answer[j] = 0;
            }
        }

        #region Set_TestData
        // Zero
        testDatas[0].nums[1, 3] = 1;
        testDatas[0].nums[1, 4] = 1;
        testDatas[0].nums[1, 5] = 1;
        testDatas[0].nums[2, 2] = 1;
        testDatas[0].nums[2, 6] = 1;
        testDatas[0].nums[3, 2] = 1;
        testDatas[0].nums[3, 6] = 1;
        testDatas[0].nums[4, 2] = 1;
        testDatas[0].nums[4, 6] = 1;
        testDatas[0].nums[5, 2] = 1;
        testDatas[0].nums[5, 6] = 1;
        testDatas[0].nums[6, 2] = 1;
        testDatas[0].nums[6, 6] = 1;
        testDatas[0].nums[7, 3] = 1;
        testDatas[0].nums[7, 4] = 1;
        testDatas[0].nums[7, 5] = 1;
        testDatas[0].answer[0] = 1;

        // One
        testDatas[1].nums[1, 4] = 1;
        testDatas[1].nums[2, 3] = 1;
        testDatas[1].nums[2, 4] = 1;
        testDatas[1].nums[3, 4] = 1;
        testDatas[1].nums[4, 4] = 1;
        testDatas[1].nums[5, 4] = 1;
        testDatas[1].nums[6, 4] = 1;
        testDatas[1].nums[7, 3] = 1;
        testDatas[1].nums[7, 4] = 1;
        testDatas[1].nums[7, 5] = 1;
        testDatas[1].answer[1] = 1;

        // Two
        testDatas[2].nums[1, 3] = 1;
        testDatas[2].nums[1, 4] = 1;
        testDatas[2].nums[1, 5] = 1;
        testDatas[2].nums[2, 2] = 1;
        testDatas[2].nums[2, 6] = 1;
        testDatas[2].nums[3, 5] = 1;
        testDatas[2].nums[4, 4] = 1;
        testDatas[2].nums[5, 3] = 1;
        testDatas[2].nums[6, 2] = 1;
        testDatas[2].nums[7, 3] = 1;
        testDatas[2].nums[7, 4] = 1;
        testDatas[2].nums[7, 5] = 1;
        testDatas[2].nums[7, 6] = 1;
        testDatas[2].answer[2] = 1;

        // Three
        testDatas[3].nums[1, 3] = 1;
        testDatas[3].nums[1, 4] = 1;
        testDatas[3].nums[1, 5] = 1;
        testDatas[3].nums[2, 2] = 1;
        testDatas[3].nums[2, 6] = 1;
        testDatas[3].nums[3, 5] = 1;
        testDatas[3].nums[4, 3] = 1;
        testDatas[3].nums[4, 4] = 1;
        testDatas[3].nums[5, 5] = 1;
        testDatas[3].nums[6, 2] = 1;
        testDatas[3].nums[6, 6] = 1;
        testDatas[3].nums[7, 3] = 1;
        testDatas[3].nums[7, 4] = 1;
        testDatas[3].nums[7, 5] = 1;
        testDatas[3].answer[3] = 1;

        // Four
        testDatas[4].nums[1, 5] = 1;
        testDatas[4].nums[2, 4] = 1;
        testDatas[4].nums[2, 5] = 1;
        testDatas[4].nums[3, 3] = 1;
        testDatas[4].nums[3, 5] = 1;
        testDatas[4].nums[4, 2] = 1;
        testDatas[4].nums[4, 5] = 1;
        testDatas[4].nums[5, 1] = 1;
        testDatas[4].nums[5, 2] = 1;
        testDatas[4].nums[5, 3] = 1;
        testDatas[4].nums[5, 4] = 1;
        testDatas[4].nums[5, 5] = 1;
        testDatas[4].nums[5, 6] = 1;
        testDatas[4].nums[5, 7] = 1;
        testDatas[4].nums[6, 5] = 1;
        testDatas[4].nums[7, 5] = 1;
        testDatas[4].answer[4] = 1;

        // Five
        testDatas[5].nums[1, 2] = 1;
        testDatas[5].nums[1, 3] = 1;
        testDatas[5].nums[1, 4] = 1;
        testDatas[5].nums[1, 5] = 1;
        testDatas[5].nums[1, 6] = 1;
        testDatas[5].nums[2, 2] = 1;
        testDatas[5].nums[3, 2] = 1;
        testDatas[5].nums[4, 3] = 1;
        testDatas[5].nums[4, 4] = 1;
        testDatas[5].nums[4, 5] = 1;
        testDatas[5].nums[5, 6] = 1;
        testDatas[5].nums[6, 2] = 1;
        testDatas[5].nums[6, 6] = 1;
        testDatas[5].nums[7, 3] = 1;
        testDatas[5].nums[7, 4] = 1;
        testDatas[5].nums[7, 5] = 1;
        testDatas[5].answer[5] = 1;

        // Six
        testDatas[6].nums[1, 3] = 1;
        testDatas[6].nums[1, 4] = 1;
        testDatas[6].nums[1, 5] = 1;
        testDatas[6].nums[2, 2] = 1;
        testDatas[6].nums[2, 6] = 1;
        testDatas[6].nums[3, 2] = 1;
        testDatas[6].nums[4, 2] = 1;
        testDatas[6].nums[4, 3] = 1;
        testDatas[6].nums[4, 4] = 1;
        testDatas[6].nums[4, 5] = 1;
        testDatas[6].nums[5, 2] = 1;
        testDatas[6].nums[5, 6] = 1;
        testDatas[6].nums[6, 2] = 1;
        testDatas[6].nums[6, 6] = 1;
        testDatas[6].nums[7, 3] = 1;
        testDatas[6].nums[7, 4] = 1;
        testDatas[6].nums[7, 5] = 1;
        testDatas[6].answer[6] = 1;

        // Seven
        testDatas[7].nums[1, 2] = 1;
        testDatas[7].nums[1, 3] = 1;
        testDatas[7].nums[1, 4] = 1;
        testDatas[7].nums[1, 5] = 1;
        testDatas[7].nums[1, 6] = 1;
        testDatas[7].nums[2, 6] = 1;
        testDatas[7].nums[3, 5] = 1;
        testDatas[7].nums[4, 5] = 1;
        testDatas[7].nums[5, 4] = 1;
        testDatas[7].nums[6, 3] = 1;
        testDatas[7].nums[7, 2] = 1;
        testDatas[7].answer[7] = 1;

        // Eight
        testDatas[8].nums[1, 3] = 1;
        testDatas[8].nums[1, 4] = 1;
        testDatas[8].nums[1, 5] = 1;
        testDatas[8].nums[2, 2] = 1;
        testDatas[8].nums[2, 6] = 1;
        testDatas[8].nums[3, 2] = 1;
        testDatas[8].nums[3, 6] = 1;
        testDatas[8].nums[4, 3] = 1;
        testDatas[8].nums[4, 4] = 1;
        testDatas[8].nums[4, 5] = 1;
        testDatas[8].nums[5, 2] = 1;
        testDatas[8].nums[5, 6] = 1;
        testDatas[8].nums[6, 2] = 1;
        testDatas[8].nums[6, 6] = 1;
        testDatas[8].nums[7, 3] = 1;
        testDatas[8].nums[7, 4] = 1;
        testDatas[8].nums[7, 5] = 1;
        testDatas[8].answer[8] = 1;

        // Nine
        testDatas[9].nums[1, 3] = 1;
        testDatas[9].nums[1, 4] = 1;
        testDatas[9].nums[1, 5] = 1;
        testDatas[9].nums[2, 2] = 1;
        testDatas[9].nums[2, 6] = 1;
        testDatas[9].nums[3, 2] = 1;
        testDatas[9].nums[3, 6] = 1;
        testDatas[9].nums[4, 3] = 1;
        testDatas[9].nums[4, 4] = 1;
        testDatas[9].nums[4, 5] = 1;
        testDatas[9].nums[5, 5] = 1;
        testDatas[9].nums[6, 4] = 1;
        testDatas[9].nums[7, 3] = 1;
        testDatas[9].answer[9] = 1;

        #endregion

        for (int i = 0; i < learningDatas.Length; i++)
        {
            learningDatas[i].nums = new float[9, 9];
            learningDatas[i].answer = new float[10];

            int randomIndex = Random.Range(0, 10);

            for (int j = 0; j < 9; j++)
            {
                for (int k = 0; k < 9; k++)
                {
                    float randomNum = Random.Range(-0.2f, 0.2f);
                    learningDatas[i].nums[j, k] = testDatas[randomIndex].nums[j, k] + randomNum;
                    if (learningDatas[i].nums[j, k] > 1)
                    {
                        learningDatas[i].nums[j, k] -= 0.2f;
                    }
                    else if (learningDatas[i].nums[j, k] < 0)
                    {
                        learningDatas[i].nums[j, k] += 0.2f;
                    }
                }
            }

            for (int j = 0; j < 10; j++)
            {
                learningDatas[i].answer[j] = testDatas[randomIndex].answer[j];
            }
        }
        #endregion
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CheckEndCalculation(int layerIndex)
    {
        List<Neuron> neurons = GetNeuronsByLayerIndex(layerIndex);
        if (neurons == null)
        {
            return;
        }
        bool flag = true;
        for (int i = 0; i < neurons.Count; i++)
        {
            if (!neurons[i].endCalculation)
            {
                flag = false;
            }
        }
        if (flag)
        {
            for (int i = 0; i < neurons.Count; i++)
            {
                neurons[i].endCalculation = false;
            }

            List<Neuron> nextNeurons;
            if (layerIndex == layerCount - 1)
            {
                count++;
                nextNeurons = GetNeuronsByLayerIndex(0);
                if (nextNeurons == null)
                {
                    return;
                }
                for (int i = 0; i < nextNeurons.Count; i++)
                {
                    if (count < learningDatas.Length)
                    {
                        print(count);
                        nextNeurons[i].a = learningDatas[count].nums[i / 9, i % 9];
                        StartCoroutine(nextNeurons[i].Load());
                    }
                }
            }
            else
            {
                nextNeurons = GetNeuronsByLayerIndex(layerIndex + 1);
                if (nextNeurons == null)
                {
                    return;
                }
                for (int i = 0; i < nextNeurons.Count; i++)
                {
                    StartCoroutine(nextNeurons[i].Load());
                }
            }
        }
    }

    public List<Neuron> GetNeuronsByLayerIndex(int layerIndex)
    {
        if (layerCount - 1 < layerIndex)
        {
            return null;
        }
        return layers[layerIndex];
    }

    public int GetLayerSizeByLayerIndex(int layerIndex)
    {
        return layers[layerIndex].Count;
    }

    public void ClickSetButton()
    {
        string layerCountText = layerCountTextField.text;
        string layerSizeText = layerSizeTextField.text;
        if (CatchIntFormatError(layerCountText) || CatchIntFormatError(layerSizeText))
        {
            return;
        }
        
        layerCount = int.Parse(layerCountText) + 2;
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

            if (i == 0)
            {
                for (int j = 0; j < 81; j++)
                {
                    Neuron neuron = Instantiate(neuronPrefab).GetComponent<Neuron>();
                    neuron.transform.position = new Vector2(-((IntervalX * (layerCount - 1)) / 2) + IntervalX * i,
                                                            -((IntervalY * (81 - 1)) / 2) + IntervalY * j);
                    neuron.layerIndex = i;
                    neuron.layer = Neuron.Layer.InputLayer;
                    layers[i].Add(neuron);
                }
            }
            else if (i == layerCount - 1)
            {
                for (int j = 0; j < 10; j++)
                {
                    Neuron neuron = Instantiate(neuronPrefab).GetComponent<Neuron>();
                    neuron.transform.position = new Vector2(-((IntervalX * (layerCount - 1)) / 2) + IntervalX * i,
                                                            -((IntervalY * (10 - 1)) / 2) + IntervalY * j);
                    neuron.layerIndex = i;
                    neuron.layer = Neuron.Layer.OutputLayer;
                    layers[i].Add(neuron);
                }
            }
            else
            {
                for (int j = 0; j < layerSize; j++)
                {
                    Neuron neuron = Instantiate(neuronPrefab).GetComponent<Neuron>();
                    neuron.transform.position = new Vector2(-((IntervalX * (layerCount - 1)) / 2) + IntervalX * i,
                                                            -((IntervalY * (layerSize - 1)) / 2) + IntervalY * j);
                    neuron.layerIndex = i;
                    neuron.layer = Neuron.Layer.HiddenLayer;
                    layers[i].Add(neuron);
                }
            }
        }
    }

    public void ClickLearnButton()
    {
        count = 0;
        learning = true;
        List<Neuron> neurons = GetNeuronsByLayerIndex(0);
        if (neurons == null)
        {
            return;
        }
        for (int i = 0; i < neurons.Count; i++)
        {
            neurons[i].a = learningDatas[count].nums[i / 9, i % 9];
            StartCoroutine(neurons[i].Load());
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
