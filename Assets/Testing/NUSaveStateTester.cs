using Nessie.Udon.SaveState;
using UdonSharp;
using UnityEngine;

public class NUSaveStateTester : UdonSharpBehaviour
{
    public NUSaveState nuSaveState;

    public ulong q0;
    public ulong q1;
    public ulong q2;
    public ulong q3;
    public ulong q4;
    public ulong q5;
    public ulong q6;
    public ulong q7;
    public ulong q8;
    public ulong q9;
    public ulong q10;
    public ulong q11;
    public ulong q12;
    public ulong q13;
    public ulong q14;
    public ulong q15;
    public ulong q16;
    public ulong q17;
    public ulong q18;
    public ulong q19;
    public ulong q20;
    public ulong q21;
    public ulong q22;
    public ulong q23;
    public ulong q24;
    public ulong q25;
    public ulong q26;
    public ulong q27;
    public ulong q28;
    public ulong q29;
    public ulong q30;
    public ulong q31;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            nuSaveState._SSLoad();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            byte b = 0;
            //generate random quaternions for q0-15
            for (int i = 0; i < 32; i++)
            {
                ulong data = 0;
                for (int j = 0; j < 8; j++)
                {
                    data |= (ulong)b << (j * 8);
                    b++;
                }
                
                SetProgramVariable("q" + i, data);
            }

            nuSaveState._SSSave();
        }
    }

    public void _SSSaved()
    {
        Debug.Log("SSSaved called");

    }
    
    public void _SSLoaded()
    {
        Debug.Log("SSLoaded called; NUSaveState output:");

        for (int i = 0; i < 32; i++)
        {
            Debug.Log($"q{i}: {GetProgramVariable("q" + i)}");
        }
    }
}
