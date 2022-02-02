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
    public bool testing;
    public int batchSize;
    float error;
    public GameObject neuronPrefab;
    public TMP_InputField layerCountTextField;
    public TMP_InputField layerSizeTextField;
    public TMP_InputField epochsTextField;
    public TMP_InputField batchSizeTextField;

    // Start is called before the first frame update
    void Start()
    {
        error = 0;
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
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public int GetLearningDatasSize()
    {
        return learningDatas.Length;
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
        string epochsText = epochsTextField.text;
        string batchSizeText = batchSizeTextField.text;

        if (CatchIntFormatError(layerCountText) || CatchIntFormatError(layerSizeText)
            || CatchIntFormatError(epochsText) || CatchIntFormatError(batchSizeText))
        {
            return;
        }
        
        layerCount = int.Parse(layerCountText) + 2;
        layerSize = int.Parse(layerSizeText);
        epochs = int.Parse(epochsText);
        batchSize = int.Parse(batchSizeText);

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
                for (int j = 80; j >= 0; j--)
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
                for (int j = 9; j >= 0; j--)
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
                for (int j = layerSize - 1; j >= 0; j--)
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
        for (int count = 0; count < epochs; count++)
        {
            for (int learningDataIndex = 0; learningDataIndex < dataSize; learningDataIndex++)
            {
                for (int layerIndex = 0; layerIndex < layers.Length; layerIndex++)
                {
                    List<Neuron> neurons = GetNeuronsByLayerIndex(layerIndex);

                    if (neurons == null)
                    {
                        return;
                    }

                    for (int i = 0; i < neurons.Count; i++)
                    {
                        if (layerIndex == 0)
                        {
                            neurons[i].a = learningDatas[learningDataIndex].nums[i / 9, i % 9];
                            neurons[i].ForwardPass();
                        }
                        else
                        {
                            List<Neuron> preNeurons = GetNeuronsByLayerIndex(layerIndex - 1);
                            for (int j = 0; j < preNeurons.Count; j++)
                            {
                                neurons[i].preActivations[j] = preNeurons[j].a;
                            }
                            neurons[i].ForwardPass();
                        }
                    }
                }

                for (int layerIndex = layers.Length - 1; layerIndex >= 0; layerIndex--)
                {
                    List<Neuron> neurons = GetNeuronsByLayerIndex(layerIndex);

                    if (neurons == null)
                    {
                        return;
                    }

                    for (int i = 0; i < neurons.Count; i++)
                    {
                        if (layerIndex == layers.Length - 1)
                        {
                            neurons[i].gradientOfActivation = 2 * (neurons[i].a - learningDatas[learningDataIndex].answer[i]);
                            error += Mathf.Pow((neurons[i].a - learningDatas[learningDataIndex].answer[i]), 2.0f);

                            if ((learningDataIndex + 1) % batchSize == 0)
                            {
                                neurons[i].BackwardPass(true);
                            }
                            else
                            {
                                neurons[i].BackwardPass(false);
                            }
                        }
                        else
                        {
                            List<Neuron> nextNeurons = GetNeuronsByLayerIndex(layerIndex + 1);
                            float temp = 0;
                            for (int j = 0; j < nextNeurons.Count; j++)
                            {
                                temp += nextNeurons[j].weights[i] * nextNeurons[j].a * (1 - nextNeurons[j].a) * nextNeurons[j].gradientOfActivation;
                            }
                            neurons[i].gradientOfActivation = temp;

                            if ((learningDataIndex + 1) % batchSize == 0)
                            {
                                neurons[i].BackwardPass(true);
                            }
                            else
                            {
                                neurons[i].BackwardPass(false);
                            }
                        }
                    }
                }
            }
            error /= learningDatas.Length;
            print((count + 1) + " " + error);
            error = 0;
        }
    }

    public void ClickTestButton()
    {
        if (!testing)
        {
            int randomIndex = Random.Range(0, learningDatas.Length);
            StartCoroutine(Test(randomIndex));
        }
    }

    IEnumerator Test(int randomIndex)
    {
        testing = true;

        print(learningDatas[randomIndex].answer[0] + " " +
            learningDatas[randomIndex].answer[1] + " " +
            learningDatas[randomIndex].answer[2] + " " +
            learningDatas[randomIndex].answer[3] + " " +
            learningDatas[randomIndex].answer[4] + " " +
            learningDatas[randomIndex].answer[5] + " " +
            learningDatas[randomIndex].answer[6] + " " +
            learningDatas[randomIndex].answer[7] + " " +
            learningDatas[randomIndex].answer[8] + " " +
            learningDatas[randomIndex].answer[9] + " "
            );

        for (int layerIndex = 0; layerIndex < layers.Length; layerIndex++)
        {
            List<Neuron> neurons = GetNeuronsByLayerIndex(layerIndex);

            for (int i = 0; i < neurons.Count; i++)
            {
                if (layerIndex == 0)
                {
                    neurons[i].a = learningDatas[randomIndex].nums[i / 9, i % 9];
                    neurons[i].ForwardPass();
                    neurons[i].sp.color = Color.white * neurons[i].a;
                    neurons[i].sp.color = new Color(neurons[i].sp.color.r, neurons[i].sp.color.g, neurons[i].sp.color.b, 1.0f);
                }
                else
                {
                    List<Neuron> preNeurons = GetNeuronsByLayerIndex(layerIndex - 1);
                    for (int j = 0; j < preNeurons.Count; j++)
                    {
                        neurons[i].preActivations[j] = preNeurons[j].a;
                    }
                    neurons[i].ForwardPass();
                    neurons[i].sp.color = Color.white * neurons[i].a;
                    neurons[i].sp.color = new Color(neurons[i].sp.color.r, neurons[i].sp.color.g, neurons[i].sp.color.b, 1.0f);
                }
            }

            yield return new WaitForSeconds(2); 

            for (int i = 0; i < neurons.Count; i++)
            {
                neurons[i].sp.color = Color.black;
            }
        }

        List<Neuron> outNeurons = GetNeuronsByLayerIndex(layers.Length - 1);

        print(outNeurons[0].a + " " +
            outNeurons[1].a + " " +
            outNeurons[2].a + " " +
            outNeurons[3].a + " " +
            outNeurons[4].a + " " +
            outNeurons[5].a + " " +
            outNeurons[6].a + " " +
            outNeurons[7].a + " " +
            outNeurons[8].a + " " +
            outNeurons[9].a + " "
            );

        testing = false;
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
