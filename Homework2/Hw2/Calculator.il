.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
    .method public hidebysig static float64
    Calculate(float64 value1, valuetype Hw2.CalculatorOperation operation, float64 value2) cil managed
    {
        ldarg.0
        ldarg.2
        
        ldarg.1
        ldc.i4.0
        beq Plus
        
        ldarg.1
        ldc.i4.1
        beq Minus
        
        ldarg.1
        ldc.i4.2
        beq Multiply
        
        ldarg.1
        ldc.i4.3
        beq Divide
        
        br Other
        
        Plus:
        add ret
        
        Minus:
        sub ret
        
        Multiply:
        mul ret
        
        Divide:
        div ret
        
        Other:
        newobj instance void [mscorlib]System.ArgumentOutOfRangeException::.ctor()
        throw
    }
}



