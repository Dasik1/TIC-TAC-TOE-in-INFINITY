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
		private bool GameFinished = false;

		private bool vsAI = true;
		private bool ShowHints = false;//-------------------------------------------------------
		private string turnAI = "o";


		private int reqursion_depth_limit = 1;
		private int step_size = 3;


		/// Время испытать удачу
		/// Подогнано с {6} раза
		private int ray_force = 1;
		private int ray_len = 5;
		private int ray_step = 1;
		private int ray_stack = 10;

		private Dictionary<Vector2, int> xWeights;
		private Dictionary<Vector2, int> oWeights;

        Vector2 win_vec;
        Vector2 win_pos;

        private List<Tuple<List<Vector2>, int>> tree;
		private Vector2 PredictedStep;

		private string turn;



		void GameStart()
		{
			GameStarted = true;
            GameFinished = false;
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
			Vector2 pos = new Vector2((int)coords.X / BoxSise, (int)coords.Y / BoxSise) + FirstBoxCords;
			


			if (xArray.Contains(pos)) { return; }
			if (oArray.Contains(pos)) { return; }

			if (turn == "x"){
				xArray.Add(pos);
				(win_vec, win_pos) = CheckWin(turn);
				turn = "o";
				CursorO();
			}else{
				oArray.Add(pos);
                (win_vec, win_pos) = CheckWin(turn);
                turn = "x";
				CursorX();
			}
			if (win_vec != new Vector2(0, 0)) {
                //DrawWin(win_vec, win_pos);///TODO
                GameFinished = true;
				Invalidate();
				return;
			}

			CalculateWeights(turn);
			if (vsAI){
				//predict step
				CalculateWeights(turn);
				PredictStep(turn, xArray, oArray, new List<Vector2>(), xWeights, oWeights);

				if (turn == "x"){
					xArray.Add(PredictedStep);
                    (win_vec, win_pos) = CheckWin(turn);
                    turn = "o";
					CursorO();
				}else{
					oArray.Add(PredictedStep);
                    (win_vec, win_pos) = CheckWin(turn);
                    turn = "x";
					CursorX();
				}


				if (win_vec != new Vector2(0, 0)) {
					GameFinished = true;
					Invalidate();
					return;
			}
				CalculateWeights(turn);
			}
			
		}

		(Vector2,Vector2) CheckWin(string turn)
		{
			List<Vector2> array;
			
            if (turn == "x") { array = xArray;}
			else { array = oArray;}
			foreach (var pos in array){
				foreach (var vec in new List<Vector2>() { new Vector2(-1, -1), new Vector2(0, -1),
														  new Vector2(1, -1), new Vector2(-1, 0)}){
					for (int i = 1; i <= 4; i++){
						if (array.Contains(pos + vec) && array.Contains(pos + 2 * vec) && array.Contains(pos + 3 * vec) && array.Contains(pos + 4 * vec)){
							return (vec,pos);
						}
					}
				}
			}
			return (new Vector2(0, 0), new Vector2(0, 0));
        }


		void CalculateWeights(string turn)
		{
			(xWeights, oWeights) = CalculateWeights(turn, xArray, oArray);
		}

        /*(Dictionary<Vector2, int>, Dictionary<Vector2, int>) CalculateWeights(string turn, List<Vector2> X, List<Vector2> O)
		{
			Dictionary<Vector2, int> xCalculatedWeights = new Dictionary<Vector2, int>();
			Dictionary<Vector2, int> oCalculatedWeights = new Dictionary<Vector2, int>();
			foreach (var pos in X){
				foreach (var vec in new List<Vector2>() { new Vector2(-1, -1)
					, new Vector2(0, -1), new Vector2(1, -1), new Vector2(-1, 0)}){
					int max, min;
					int force = ray_force;
					for (max = 1; max < ray_len; max++){
						if (O.Contains(pos + max * vec)) { max -= 1; break; }
						if (X.Contains(pos + max * vec)) { force += ray_force; force *= ray_stack; }
					}
					for (min = 1; min < ray_len; min++){
						if (O.Contains(pos - min * vec)) { min -= 1; break; }
						if (X.Contains(pos - min * vec)) { force += ray_force; force *= ray_stack; }
					}
					

					for (var i = -min; i <= max; i++){
						if (i == 0) { continue; }
						if (X.Contains(pos + i * vec)) { continue; }
						if (O.Contains(pos + i * vec)) { continue; }
						if (xCalculatedWeights.ContainsKey(pos + vec * i)) { xCalculatedWeights[pos + vec * i] = Math.Max(xCalculatedWeights[pos + vec * i],force - Math.Abs(i) * ray_step)+1; }
						else { xCalculatedWeights.Add(pos + vec * i, force - Math.Abs(i) * ray_step); }
					}
				}
			}

			foreach (var pos in O){
				foreach (var vec in new List<Vector2>() { new Vector2(-1, -1)
					, new Vector2(0, -1), new Vector2(1, -1), new Vector2(-1, 0) }){
					int max, min;
					int force = ray_force;
					for (max = 1; max < ray_len; max++){
						if (X.Contains(pos + max * vec)) { max -= 1;  break; }
						if (O.Contains(pos + max * vec)) { force += ray_force; force *= ray_stack; }
					}
					for (min = 1; min < ray_len; min++){
						if (X.Contains(pos - min * vec)) { min -= 1; break; }
						if (O.Contains(pos - min * vec)) { force += ray_force; force *= ray_stack; }
					}
					

					for (var i = -min; i <= max; i++){
						if (i == 0) { continue; }
						if (O.Contains(pos + i * vec)) { continue; }
						if (X.Contains(pos + i * vec)) { continue; }
						if (oCalculatedWeights.ContainsKey(pos + vec * i)) { oCalculatedWeights[pos + vec * i] = Math.Max(oCalculatedWeights[pos + vec * i],force - Math.Abs(i) * ray_step)+1; }
						else { oCalculatedWeights.Add(pos + vec * i, force - Math.Abs(i) * ray_step); }
					}
				}
			}
			return (xCalculatedWeights, oCalculatedWeights);
		}*/













        (Dictionary<Vector2, int>, Dictionary<Vector2, int>) CalculateWeights(string turn, List<Vector2> X, List<Vector2> O)
        {

            Dictionary<Vector2, int> xCalculatedWeights = new Dictionary<Vector2, int>();
            Dictionary<Vector2, int> oCalculatedWeights = new Dictionary<Vector2, int>();

            foreach (var pos in X){
                foreach (var vec in new List<Vector2>() { new Vector2(1, 0),new Vector2(0, 1),
														  new Vector2(1, -1), new Vector2(-1, -1)}){

                    var VisionRay = new List<string>();
                    var DeltLeft = 0;
                    for (int i = 0; i < ray_len; i++){
                        DeltLeft += 1;
                        if (O.Contains(pos - i * vec)) { VisionRay.Add("o"); break; }
                        
                        if (X.Contains(pos - i * vec)) { VisionRay.Add("x"); }
                        else { VisionRay.Add(" "); }
                    }

                    VisionRay.Reverse();

                    for (int i = 1; i < ray_len; i++){
                        if (O.Contains(pos + i * vec)) { VisionRay.Add("o"); break; }
                        if (X.Contains(pos + i * vec)) { VisionRay.Add("x"); }
                        else { VisionRay.Add(" "); }
                    }

					if (!VisionRay.Contains(" ")) { continue; }
					var f = ray_force * (int) Math.Pow(ray_stack, VisionRay.Count(c => c == "x"));


					for (var i = 0; i < VisionRay.Count; i++){
						var Delt = i + 1 - DeltLeft;
                        var _x = pos + (i+1-DeltLeft) * vec;
						if (X.Contains(_x)) { continue; }	
						if (O.Contains(_x)) { continue; }	
						if (xCalculatedWeights.ContainsKey(_x)) { xCalculatedWeights[_x] += f- Math.Abs(Delt)*ray_step;  }
						else { xCalculatedWeights.Add(_x, f - Math.Abs(Delt)*ray_step); }
						
					}
                }
            }
            foreach (var pos in O){
                foreach (var vec in new List<Vector2>() { new Vector2(1, 0),new Vector2(0, 1),
														  new Vector2(1, -1), new Vector2(-1, -1)}){

                    var VisionRay = new List<string>();
                    var DeltLeft = 0;
                    for (int i = 0; i < ray_len; i++){
                        DeltLeft += 1;
                        if (X.Contains(pos - i * vec)) { VisionRay.Add("x"); break; }
                        if (O.Contains(pos - i * vec)) { VisionRay.Add("o"); }
                        else { VisionRay.Add(" "); }
                    }

                    VisionRay.Reverse();

                    for (int i = 1; i < ray_len; i++){
                        if (X.Contains(pos + i * vec)) { VisionRay.Add("x"); break; }
                        if (O.Contains(pos + i * vec)) { VisionRay.Add("o"); }
                        else { VisionRay.Add(" "); }
                    }

					if (!VisionRay.Contains(" ")) { continue; }
					var f = ray_force * (int) Math.Pow(ray_stack, VisionRay.Count(c => c == "o"));


					for (var i = 0; i < VisionRay.Count; i++){
						var Delt = i + 1 - DeltLeft;
                        var _x = pos + (i+1-DeltLeft) * vec;
						if (O.Contains(_x)) { continue; }	
						if (X.Contains(_x)) { continue; }	
						if (oCalculatedWeights.ContainsKey(_x)) { oCalculatedWeights[_x] += f - Math.Abs(Delt) * ray_step; }
						else { oCalculatedWeights.Add(_x, f - Math.Abs(Delt) * ray_step); }
						
					}
                }
            }
            return (xCalculatedWeights, oCalculatedWeights);
        }







        void PredictStep(string turn,
						List<Vector2> X, List<Vector2> O,
						List<Vector2> Path,
						Dictionary<Vector2, int> xCalculatedWeights, Dictionary<Vector2, int> oCalculatedWeights,
						int ReqursionDepth = 0, int Value = 0)
		{
			///1//if depth is max - add path to tree
			///2//Depth - 0 => choose best opportunires
			///21//top and bottom border
			///22//get some best pos for X O and BOTH
			///3//look for their future
			///31//copy
			///32//expand
			///33//recalc and edit "turn"
			///34//uve been caught in reqursion
			///4//when tree is done (Depth - 0)
			///5//look for the best val
			///6//return that


			//1
			if (ReqursionDepth == reqursion_depth_limit) { tree.Add(new Tuple<List<Vector2>, int>(Path, Value)); return; }

			//21
			if (ReqursionDepth == 0) { tree = new List<Tuple<List<Vector2>, int>>(); }

			var xLow = xCalculatedWeights.Values.Distinct().Order().Reverse().ToArray().First();
			if (xCalculatedWeights.Values.Distinct().ToArray().Count()>step_size) {
				xLow = xCalculatedWeights.Values.Distinct().Order().Reverse().ToArray()[..step_size].Last();
			}


			int oLow = 0;
			if (oCalculatedWeights.Count != 0){
				oLow = oCalculatedWeights.Values.Distinct().Order().Reverse().ToArray().First();
				if (oCalculatedWeights.Values.Distinct().ToArray().Count() > step_size){
					oLow = oCalculatedWeights.Values.Distinct().Order().Reverse().ToArray()[..step_size].Last();
				}
			}

			//22
			Dictionary<Vector2, int> xBestPos = new Dictionary<Vector2, int>();
			Dictionary<Vector2, int> oBestPos = new Dictionary<Vector2, int>();
			Dictionary<Vector2, int> xoBestPos = new Dictionary<Vector2, int>();

			//here we ll add to it weights of BOTH so no need to new cycle
			Dictionary<Vector2, int> xoCalculatedWeights = new Dictionary<Vector2, int>();

			foreach (var x in xCalculatedWeights){
				if (x.Value >= xLow){
					xBestPos.Add(x.Key, x.Value);
					if (oCalculatedWeights.ContainsKey(x.Key)) { xoCalculatedWeights.Add(x.Key, x.Value + oCalculatedWeights[x.Key]); }
				}
			}
			//same for O
			foreach (var o in oCalculatedWeights){
				if (o.Value >= oLow){
					oBestPos.Add(o.Key, o.Value);
					if (xCalculatedWeights.ContainsKey(o.Key) && !xoCalculatedWeights.ContainsKey(o.Key)) { xoCalculatedWeights.Add(o.Key, o.Value + xCalculatedWeights[o.Key]); }
				}
			}

			//for BOTH
			//--calc best
			var xoLow = -1;
			if (xoCalculatedWeights.Values.Distinct().Count() == 1){
				xoLow = xoCalculatedWeights.Values.Distinct().Order().Reverse().ToArray().First();
			} else if (xoCalculatedWeights.Values.Distinct().Count() > step_size){
				xoLow = xoCalculatedWeights.Values.Distinct().Order().Reverse().ToArray()[..step_size].Last();
			}

			//--choose vecs
			foreach (var xo in xoCalculatedWeights){
				if (xo.Value > xoLow){
					xoBestPos.Add(xo.Key, xo.Value);
				}
			}


			//3
			//PREDICTION
			//
			//lil bracket spagetty not to catch same key error))
			var MovesUnion = (xBestPos.ToList()).Union(oBestPos.ToList()).ToList();
			MovesUnion.Union(xoBestPos.ToList()).ToList();

			foreach (var move in MovesUnion) {
				//31 (in circle use only copies)
				var newTurn = turn;
				var newX = new List<Vector2>(X);
				var newO = new List<Vector2>(O);
				var newPath = new List<Vector2>(Path);

				//32
				if (newTurn == "x") { newX.Add(move.Key); }
				else { newO.Add(move.Key); }
				newPath.Add(move.Key);
				var newRecievedPoints = move.Value;

				//33
				(var newxCalculatedWeights, var newoCalculatedWeights) = CalculateWeights(turn, newX, newO);
				newTurn = (newTurn == "o") ? "x" : "o";

				//34
				// reqursion step is different iv we predict our(1) or Player(0) step
				if (newTurn == turnAI) {
					PredictStep(newTurn, newX, newO, newPath,
								newxCalculatedWeights, newoCalculatedWeights,
								ReqursionDepth+1, Value+newRecievedPoints);
				}else{
					PredictStep(newTurn, newX, newO, newPath,
								newxCalculatedWeights, newoCalculatedWeights,
								ReqursionDepth + 1, Value+newRecievedPoints);
				}
			}

			//4
			if (ReqursionDepth != 0) { return; }

			//5
			var BestScorePair = tree.OrderBy(i => i.Item2).Reverse().ToArray().First();

			//6
			PredictedStep = BestScorePair.Item1.First();
		}
	}
}