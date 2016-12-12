using UnityEngine;
using System.Linq;
using System.Collections;
using LitJson;

public class DiagonalTable : ScriptableObject
{
    Table table;


    [ContextMenu("Test0")]
    public void Test0()
    {
        //JsonData jsonData;
        //Table.Ser ser = new Table.Ser(Table.InformationCell)
        //jsonData = JsonMapper.ToJson();

    }

    [ContextMenu("Test1")]
    public void Test1()
    {
        table = new Table(3);

    }

    [ContextMenu("Test2")]
    public void Test2()
    {
        table.At((int)FactionEnum.Player, (int)FactionEnum.Pirate).Value += -100;
        Show(table, (int)FactionEnum.Player, (int)FactionEnum.Pirate);
    }

    [ContextMenu("Test3")]
    public void Test3()
    {
        Show(table, (int)FactionEnum.Player, (int)FactionEnum.Pirate);
    }
    private static void Show(Table table, int i, int j)
    {
        Debug.Log("" + i + " " + j + ": " + table.At(i, j).Value);
    }

    public class Table
    {
        public class InformationCell
        {
            private readonly int _indexA;
            private readonly int _indexB;
            private float _value;

            public float GetValue(int i, int j)
            {
                return i != _indexA ? _value : _value;
            }

            public void SetValue(int i, int j, float value)
            {
                _value = i != _indexA ? value : value;
            }

            public InformationCell(int a, int b)
            {
                _indexA = a;
                _indexB = b;
            }
        }

        public class CellAccess
        {
            private readonly InformationCell _cell;
            private readonly int _i;
            private readonly int _j;
            public CellAccess(InformationCell cell, int i, int j)
            {
                _cell = cell;
                _i = i;
                _j = j;
            }
            public float Value
            {
                get
                {
                    return _cell.GetValue(_i, _j);
                }
                set
                {
                    _cell.SetValue(_i, _j, value);
                }
            }
        }

        private readonly InformationCell[][] _data;

        public CellAccess At(int i, int j)
        {
            return new CellAccess(_data[i][j], i, j);
        }

        public Table(int size)
        {
            _data = Enumerable.Range(0, size).Select(x => new InformationCell[size]).ToArray();
            for (int i = 0; i < size; ++i)
            {
                for (int j = i; j < size; ++j)
                {
                    _data[i][j] = new InformationCell(i, j);
                    if (i != j)
                    {
                        _data[j][i] = _data[i][j];
                    }
                }
            }
        }

        public class Ser
        {
            public InformationCell info;

            public Ser(InformationCell inf)
            {
                this.info = inf;
            }
        }

    }

 
    

}
