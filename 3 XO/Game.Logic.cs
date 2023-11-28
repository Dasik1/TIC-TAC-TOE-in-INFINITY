using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Windows.Forms;


namespace _3_XO
{
    partial class Game
    {

        private Vector2 FirstBoxCords = new Vector2(100, 100);

        private List<Vector2> xArray;
        private List<Vector2> oArray;

        private bool GameStarted = false;

        private bool vsAI = true;
        private bool ShowHints;
        private string turnAI = "o";


        private int reqursion_depth_limit = 3;
        private int step_size = 4;

        
        /// интуиция и эксперименты
        /// подогнано с {3} раза
        private int ray_force = 5;
        private int ray_len = 5;
        private int ray_step = 1;
        private int ray_stack = 2;

        private Dictionary<Vector2, int>  xWeights;
        private Dictionary<Vector2, int>  oWeights;

        private List<Tuple<List<Vector2>,int>> tree;

        private string turn; 





        void GameStart()
        {
            GameStarted = true;
            turn = "x";
            CursorX();

            xArray = new List<Vector2>();
            oArray = new List<Vector2>();

            xWeights = new Dictionary<Vector2, int>();
            oWeights = new Dictionary<Vector2, int>();

            Invalidate();
        }

        private void UpdateSettings()
        {
            vsAI = vsComputer.Checked;
            ShowHints = Hints.Checked;
        }

        void GameStep(Vector2 coords)
        {
            Vector2 pos = new Vector2((int)coords.X/BoxSise, (int)coords.Y / BoxSise) + FirstBoxCords;

            if (xArray.Contains(pos)) { return; }
            if (oArray.Contains(pos)) { return; }


            if (turn == "x")
            {
                xArray.Add(pos);
                turn = "o";
                CursorO();
            }
            else{
                oArray.Add(pos);
                turn = "x";
                CursorX();
            }


            CalculateWeights(turn);

            if (vsAI){
                ChooseSteps(turn, xArray, oArray, new List<Vector2>(), xWeights, oWeights);
                if (turn == "x"){
                    turn = "o";
                    CursorO();
                }
                else{
                    turn = "x";
                    CursorX();
                }
            }
            //CheckWin();
        }


        (Dictionary<Vector2, int>, Dictionary<Vector2, int>) CalculateWeights(string turn, List<Vector2> X, List<Vector2> O)
        {
            Dictionary<Vector2, int> xCalculatedWeights = new Dictionary<Vector2, int>();
            Dictionary<Vector2, int> oCalculatedWeights = new Dictionary<Vector2, int>();
            foreach (var pos in X){
                foreach (var vec in new List<Vector2>() { new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1), new Vector2(-1, 0) }){
                    int max, min;
                    int force = ray_force;
                    for (max = 1; max < ray_len; max++){
                        if (O.Contains(pos + max * vec)) { max = -max + 1; break; }
                        if (X.Contains(pos + max * vec)) { force += ray_force; force *= ray_stack; }
                    }
                    for (min = 1; min < ray_len; min++){
                        if (O.Contains(pos - min * vec)) { min = -min + 1; break; }
                        if (X.Contains(pos - min * vec)) { force += ray_force; force *= ray_stack; }
                    }
                    if ((max < 0 && min < 0) && (-min - max < 5 )){
                        ///с обоих сторон "o" и длина между ними меньше победной
                        ///немного оптимизации
                        force = -ray_force;
                    }
                    if (min < 0) { min *=-1; }
                    if (max < 0) { max *=-1; }

                    for (var i = -min; i <= max; i++){
                        if (i == 0) { continue; }
                        if (X.Contains(pos + i * vec)) { continue; }
                        if (xCalculatedWeights.ContainsKey(pos + vec * i)) { xCalculatedWeights[pos + vec * i] += force - Math.Abs(i) * ray_step; }
                        else { xCalculatedWeights.Add(pos + vec * i, force - Math.Abs(i) * ray_step); }
                    }
                }
            }

            foreach (var pos in O){
                foreach (var vec in new List<Vector2>() { new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1), new Vector2(-1, 0) }){
                    int max, min;
                    int force = ray_force;
                    for (max = 1; max < ray_len; max++){
                        if (X.Contains(pos + max * vec)) { max = -max + 1; break; }
                        if (O.Contains(pos + max * vec)) { force += ray_force; force *= ray_stack; }
                    }
                    for (min = 1; min < ray_len; min++){
                        if (X.Contains(pos - min * vec)) { min = -min + 1; break; }
                        if (O.Contains(pos - min * vec)) { force += ray_force; force *= ray_stack; }
                    }
                    if ((max < 0 && min < 0) && (-min - max < 5 )){
                        ///с обоих сторон "o" и длина между ними меньше победной
                        ///немного оптимизации
                        force = -ray_force*ray_step;
                    }
                    if (min < 0) { min *=-1; }
                    if (max < 0) { max *=-1; }

                    for (var i = -min; i <= max; i++){
                        if (i == 0) { continue; }
                        if (O.Contains(pos + i * vec)) { continue; }
                        if (oCalculatedWeights.ContainsKey(pos + vec * i)) { oCalculatedWeights[pos + vec * i] += force - Math.Abs(i) * ray_step; }
                        else { oCalculatedWeights.Add(pos + vec * i, force - Math.Abs(i) * ray_step); }
                    }
                }
            }
            return (xCalculatedWeights, oCalculatedWeights);
        }


        void CalculateWeights(string turn)
        {
            (xWeights, oWeights) = CalculateWeights(turn, xArray, oArray);
        }

        




        void ChooseSteps(string turn,
                        List<Vector2> X, List<Vector2> O,
                        List<Vector2> Path,
                        Dictionary<Vector2, int> xCalculatedWeights, Dictionary<Vector2, int> oCalculatedWeights,
                        int ReqursionDepth = 0, int Value = 0 )
        {
            if ( ReqursionDepth == 0 ) { tree = new List<Tuple<List<Vector2>, int>>(); }
            if ( ReqursionDepth == reqursion_depth_limit ) { tree.Add(new Tuple<List<Vector2>, int>(Path, Value)); return; }

            var xLow = xCalculatedWeights.Values.Distinct().Order().Reverse().ToArray().First();
            var xHigh = xCalculatedWeights.Values.Distinct().Order().Reverse().ToArray().First();
            if (xCalculatedWeights.Distinct().ToArray().Count()>step_size) {
                xLow = xCalculatedWeights.Values.Distinct().Order().Reverse().ToArray()[..step_size].Last();
                xHigh = xCalculatedWeights.Values.Distinct().Order().Reverse().ToArray()[..step_size].First();}

            int oLow = 0;
            int oHigh = 0;
            if (oCalculatedWeights.Count != 0){ 
                oLow = oCalculatedWeights.Values.Distinct().Order().Reverse().ToArray().First();
                oHigh = oCalculatedWeights.Values.Distinct().Order().Reverse().ToArray().First();
                if (oCalculatedWeights.Distinct().ToArray().Count() > step_size){
                    oLow = oCalculatedWeights.Values.Distinct().Order().Reverse().ToArray()[..step_size].Last();
                    oHigh = oCalculatedWeights.Values.Distinct().Order().Reverse().ToArray()[..step_size].First();}
            }

            Dictionary<Vector2, int> xBestPos = new Dictionary<Vector2, int>();
            Dictionary<Vector2, int> oBestPos = new Dictionary<Vector2, int>();
            Dictionary<Vector2, int> xoBestPos = new Dictionary<Vector2, int>();
            Dictionary<Vector2, int> xoCalculatedWeights = new Dictionary<Vector2, int>();

            foreach (var x in xCalculatedWeights){
                if (x.Value > xLow){
                    xBestPos.Add(x.Key, x.Value);
                    if (oCalculatedWeights.ContainsKey(x.Key)) { xoCalculatedWeights.Add(x.Key, x.Value + oCalculatedWeights[x.Key]); }
                }
            }

            foreach (var o in oCalculatedWeights){
                if (o.Value > oLow){
                    oBestPos.Add(o.Key, o.Value);
                    if (xCalculatedWeights.ContainsKey(o.Key) && !xoCalculatedWeights.ContainsKey(o.Key)) { xoCalculatedWeights.Add(o.Key, o.Value + xCalculatedWeights[o.Key]); }
                }
            }
            var xoLow = -1;
            if (xoCalculatedWeights.Values.Distinct().Count() > step_size){
                xoLow = xoCalculatedWeights.Values.Distinct().Order().Reverse().ToArray()[..step_size].Last();
            }

            foreach (var xo in xoCalculatedWeights){
                if (xo.Value > xoLow){
                    xoBestPos.Add(xo.Key, xo.Value);
                }
            }


            //make turn
            //recalc weights
            //tree step

            foreach (var move in xBestPos.Union(oBestPos).Union(xoBestPos).ToList()) {
                var newTurn = turn;
                var newX = new List<Vector2>(X);
                var newO= new List<Vector2>(O);
                var newPath= new List<Vector2>(Path);
                //make turn
                newPath.Add(move.Key);
                if (turn == "x") { newX.Add(move.Key); }
                else{ newO.Add(move.Key); }
                var AddedValue = move.Value;

                //recalc weights
                (var newxCalculatedWeights, var newoCalculatedWeights) = CalculateWeights(turn, newX, newO);


                //tree step
                if (newTurn == "x") { newTurn = "o"; }
                else { newTurn = "x"; }
                if (turn == turnAI){
                    ChooseSteps(turn, newX, newO, newPath,
                            newxCalculatedWeights, newoCalculatedWeights,
                            ReqursionDepth + 1, Value + AddedValue);
                }
                else{
                    ChooseSteps(turn, newX, newO, newPath,
                            newxCalculatedWeights, newoCalculatedWeights,
                            ReqursionDepth + 1, Value);
                }
            }
            if (ReqursionDepth != 0) { return; }

            var BestScore = tree.OrderBy(i => i.Item2).Reverse().ToList().First();
            if (turn == "x") { xArray.Add(BestScore.Item1[0]); }
            else { oArray.Add(BestScore.Item1[0]); }

        }
    }
}