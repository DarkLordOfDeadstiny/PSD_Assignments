namespace Ex1_4;

public abstract class Expr
{
    public abstract int eval(IDictionary<string, int> env);
    public abstract Expr simplify();
} 


public class CstI : Expr
{
    public int _i { get; init; }
    public CstI(int i)
    {
        _i = i;
    }

    public override int eval(IDictionary<string, int> env) => _i;

    public override Expr simplify() => this;

    public override string ToString() => _i.ToString();
}

public class Var : Expr
{
    public string _s { get; init; }
    public Var(string s)
    {
        _s = s;
    }

    public override int eval(IDictionary<string, int> env)
    {
        return env[_s];
    }

    public override string ToString() => _s;

    public override Expr simplify() => this;
}

public abstract class Binop : Expr
{

}

public class Add : Binop
{
    public Expr _e1, _e2;
    public Add(Expr e1, Expr e2)
    {
        _e1 = e1;
        _e2 = e2;
    }

    public override int eval(IDictionary<string, int> env)
    {
        return _e1.eval(env) + _e2.eval(env);
    }

    public override Expr simplify()
    {
        var r1 = _e1.simplify();
        var r2 = _e2.simplify();
        return (r1, r2) switch
        {
            (CstI { _i: 0},_) => r2,
            (_, CstI { _i: 0 }) => r1,
            _ => this
        };

    }

    public override string ToString() => $"({_e1} + {_e2})";
}

public class Mul : Binop
{
    public Expr _e1, _e2;
    public Mul(Expr e1, Expr e2)
    {
        _e1 = e1;
        _e2 = e2;
    }

    public override int eval(IDictionary<string, int> env)
    {
        return _e1.eval(env) * _e2.eval(env);
    }

    public override Expr simplify()
    {
        var r1 = _e1.simplify();
        var r2 = _e2.simplify();
        return (r1, r2) switch
        {
            (CstI { _i: 0 }, _) => new CstI(0),
            (_, CstI { _i: 0 }) => new CstI(0),
            (CstI { _i: 1 }, _) => r2,
            (_, CstI { _i: 1 }) => r1,
            _ => this
        };
    }

    public override string ToString() => $"({_e1} * {_e2})";
}

public class Sub : Binop
{
    public Expr _e1, _e2;
    public Sub(Expr e1, Expr e2)
    {
        _e1 = e1;
        _e2 = e2;
    }

    public override int eval(IDictionary<string, int> env)
    {
        return _e1.eval(env) - _e2.eval(env);
    }

    public override Expr simplify()
    {
        var r1 = _e1.simplify();
        var r2 = _e2.simplify();
        return (r1, r2) switch
        {
            (_, CstI { _i: 0 }) => r1,
            _ => r1 == r2 ? new CstI(0) : this
        };
    }

    public override string ToString() => $"({_e1} - {_e2})";
}

