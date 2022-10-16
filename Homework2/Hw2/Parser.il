.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public hidebysig static void
    ParseCalcArguments(
    string[] args,
    [out] float64& val1,
    [out] valuetype Hw2.CalculatorOperation& operation,
    [out] float64& val2) cil managed
    {
        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse WrongArgs
        
        ldarg.0
        ldc.i4.0
        ldelem.ref
        ldarg.1
        call bool [mscorlib]System.Double::TryParse(string, float64&)
        brfalse.s WrongArgs
        
        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call bool [mscorlib]System.Double::TryParse(string, float64&)
        brfalse.s WrongArgs
        
        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
        
        ret
        
        WrongArgs:
        newobj instance void [mscorlib]System.ArgumentException::.ctor()
        throw
    }
  
    .method private hidebysig static bool
    IsArgLengthSupported(string[] args) cil managed
    {
        ldarg.0
        ldlen
        conv.i4
        ldc.i4 3
        ceq
        ret
    }
  
    .method private hidebysig static valuetype Hw2.CalculatorOperation
    ParseOperation(string arg) cil managed
    {
        .locals init (valuetype Hw2.CalculatorOperation)
    
        ldarg.0
        ldstr "+"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue Plus
        
        ldarg.0
        ldstr "-"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue Minus
        
        ldarg.0
        ldstr "*"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue Multiply
        
        ldarg.0
        ldstr "/"
        call bool [mscorlib]System.String::op_Equality(string, string)
        brtrue Divide
        
        br Other
        
        Plus:
        ldc.i4.0
        ret
        
        Minus:
        ldc.i4.1
        ret
        
        Multiply:
        ldc.i4.2
        ret
        
        Divide:
        ldc.i4.3
        ret
        
        Other:
        newobj instance void [mscorlib]System.InvalidOperationException::.ctor()
        throw
    }
}