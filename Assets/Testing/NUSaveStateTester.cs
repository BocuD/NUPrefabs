using System;
using Nessie.Udon.SaveState;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum SaveMode
{
    Quaternion,
    Ulong,
    Binary
}

public class NUSaveStateTester : UdonSharpBehaviour
{
    public NUSaveState quaternionSaveState;
    public NUSaveState ulongSaveState;
    public NUSaveState binarySaveState;

    public bool generateRandom = false;
    public Toggle generateRandomToggle;
    
    SaveMode saveMode = SaveMode.Quaternion;
    
    public ulong u0;
    public ulong u1;
    public ulong u2;
    public ulong u3;
    public ulong u4;
    public ulong u5;
    public ulong u6;
    public ulong u7;
    public ulong u8;
    public ulong u9;
    public ulong u10;
    public ulong u11;
    public ulong u12;
    public ulong u13;
    public ulong u14;
    public ulong u15;
    public ulong u16;
    public ulong u17;
    public ulong u18;
    public ulong u19;
    public ulong u20;
    public ulong u21;
    public ulong u22;
    public ulong u23;
    public ulong u24;
    public ulong u25;
    public ulong u26;
    public ulong u27;
    public ulong u28;
    public ulong u29;
    public ulong u30;
    public ulong u31;

    public Quaternion[] targetQuaternions;
    
    public Quaternion q0;
    public Quaternion q1;
    public Quaternion q2;
    public Quaternion q3;
    public Quaternion q4;
    public Quaternion q5;
    public Quaternion q6;
    public Quaternion q7;
    public Quaternion q8;
    public Quaternion q9;
    public Quaternion q10;
    public Quaternion q11;
    public Quaternion q12;
    public Quaternion q13;
    public Quaternion q14;
    public Quaternion q15;

    public byte[] targetBytes;

    public byte b0;
    public byte b1;
    public byte b2;
    public byte b3;
    public byte b4;
    public byte b5;
    public byte b6;
    public byte b7;

    public void UpdateRandomState()
    {
        generateRandom = generateRandomToggle.isOn;
    }
    
    public void SetQuaternionMode()
    {
        saveMode = SaveMode.Quaternion;
        
        quaternionSaveState.console.text += "Switched to Quaternion mode\n";
    }
    
    public void SetUlongMode()
    {
        saveMode = SaveMode.Ulong;
        
        ulongSaveState.console.text += "Switched to Ulong mode\n";
    }
    
    public void SetBinaryMode()
    {
        saveMode = SaveMode.Binary;
        
        quaternionSaveState.console.text += "Switched to Binary mode\n";
    }

    public void Save()
    {
        switch (saveMode)
        {
            case SaveMode.Quaternion:

                quaternionSaveState.console.text = "";
                
                if (generateRandom)
                {
                    //seed the unity random with time
                    Random.InitState(DateTime.Now.Millisecond);
                    
                    //get a seed for the quaternion random with the primed random
                    int seed = Random.Range(int.MinValue, int.MaxValue);
                    
                    //seed the unity random with the int
                    Random.InitState(seed);
                    
                    quaternionSaveState.console.text += "Generated random seed: " + seed + "\n";
                    
                    targetQuaternions = new Quaternion[16];
                    //gerenate random quaternions for targetQuaternions
                    for (int i = 0; i < 16; i++)
                    {
                        targetQuaternions[i] = UnityEngine.Random.rotation;
                    }
                }

                //fill q0-q15 with targetQuaternions
                for (int i = 0; i < 16; i++)
                {
                    SetProgramVariable("q" + i, targetQuaternions[i]);
                }
                
                quaternionSaveState._SSSave();
                break;
            
            case SaveMode.Ulong:
                
                //generate increasing ulongs for u0-31
                ulong b = 0;   
                for (int i = 0; i < 32; i++)
                {
                    ulong data = 0;
                    for (int j = 0; j < 8; j++)
                    {
                        data |= b << (j * 8);
                        b++;
                    }
                
                    SetProgramVariable("u" + i, data);
                }
                
                ulongSaveState.console.text = "";
                ulongSaveState._SSSave();
                break;
            
            case SaveMode.Binary:
                
                //fill b0-b7 with the byte array
                for(int i = 0; i < 8; i++)
                {
                    SetProgramVariable("b" + i, targetBytes[i]);
                }

                Debug.Log(b1.GetType());
                
                binarySaveState.console.text = "";
                binarySaveState._SSSave();
                break;
        }
    }

    public void Load()
    {
        switch (saveMode)
        {
            case SaveMode.Quaternion:
                quaternionSaveState.console.text = "";
                quaternionSaveState._SSLoad();
                break;
            
            case SaveMode.Ulong:
                ulongSaveState.console.text = "";
                ulongSaveState._SSLoad();
                break;
            
            case SaveMode.Binary:
                binarySaveState.console.text = "";
                binarySaveState._SSLoad();
                break;
        }
    }

    public void _SSSaved()
    {
        Debug.Log("SSSaved called");
    }
    
    public void _SSLoaded()
    {
        Debug.Log("SSLoaded called; NUSaveState output:");

        switch (saveMode)
        {
            case SaveMode.Quaternion:
                for (int i = 0; i < 16; i++)
                {
                    Debug.Log($"q{i}: {GetProgramVariable("q" + i)}");
                }
                break;
            
            case SaveMode.Ulong:
                for (int i = 0; i < 32; i++)
                {
                    Debug.Log($"u{i}: {GetProgramVariable("u" + i)}");
                }
                break;
            
            case SaveMode.Binary:
                for (int i = 0; i < 8; i++)
                {
                    Debug.Log($"b{i}: {GetProgramVariable("b" + i)}");
                }
                break;
        }
    }
}
