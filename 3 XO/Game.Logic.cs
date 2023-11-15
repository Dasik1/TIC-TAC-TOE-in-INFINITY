using System.ComponentModel.Design.Serialization;
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

        bool GameStarted = false;

        bool vsAI;
        bool ShowHints;


        
        /// интуиция и эксперименты
        /// подогнано с {4} раза
        int ray_force = 5;
        int ray_len = 5;
        int ray_step = 3;
        int ray_stack = 2;
        float greed = 0.1f;

        Dictionary<Vector2, int>  xCalculatedWeights;
        Dictionary<Vector2, int>  oCalculatedWeights;

        private string turn; 





        void GameStart()
        {
            GameStarted = true;
            turn = "x";
            CursorX();

            xArray = new List<Vector2>();
            oArray = new List<Vector2>();

            xCalculatedWeights = new Dictionary<Vector2, int>();
            oCalculatedWeights = new Dictionary<Vector2, int>();

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
                if (vsAI){
                    oArray.Add(pos);///-----------------------------------------------------------------------------------------------

                    turn = "x";
                    CursorX();
                }else{
                    oArray.Add(pos);
                    turn = "x";
                    CursorX();
                }
            }
            CalculateWeights(turn);
            //CheckWin();
        }

        void CalculateWeights(string turn)
        {
            xCalculatedWeights.Clear();
            oCalculatedWeights.Clear();

            ///если на пути распространения есть та же метка то луч усиливается
            ///другая - тормозится
            ///
            ///есть теория что лучший ход для Х - поставить на лучшее место для О
            ///если для себя ставить некуда))
            ///
            ///пройтись по направлению до конца луча длины {ray_len} или до другого символа
            ///посчитать число элементов
            ///прокрасить все поля от до весом удаляющимся от центра
            ///важно что при стрике ход должен быть в приоритете
            {
                foreach (var pos in xArray)
                {
                    ///пройтись по направлению до конца луча длины {ray_len} или до другого символа
                    ///посчитать число элементов
                    ///прокрасить все поля от до весом удаляющимся от центра
                    ///важно что при стрике ход должен быть в приоритете


                    foreach (var vec in new List<Vector2>() { new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1), new Vector2(-1, 0) }){
                        int i;
                        Debug.WriteLine("Calc begin");
                        for (i = 1; i < ray_len; i++){
                            Debug.Write(i);
                            Debug.Write("-");
                            if (oArray.Contains(pos + i * vec)){
                                i--;
                                break;
                            }
                        }
                        Debug.WriteLine("");
                        int force = 0;
                        for (; i > -ray_len; i--){
                            Debug.Write(i);
                            Debug.Write("-");
                            if (oArray.Contains(pos + i * vec)){
                                i++;
                                break;
                            }
                            if (xArray.Contains(pos + i * vec)) { force += ray_force; force *= ray_stack; }
                        }
                        Debug.WriteLine("");
                        for (; i <= ray_len; i++){
                            if (xArray.Contains(pos + i * vec)) { continue; }
                            if (oArray.Contains(pos + i * vec)) { break; }
                            Debug.Write(i);
                            Debug.Write("-");
                            if (xCalculatedWeights.ContainsKey(pos + vec * i)) { xCalculatedWeights[pos + vec * i] += force - Math.Abs(i) * ray_step; }
                            else { xCalculatedWeights.Add(pos + vec * i, force - Math.Abs(i) * ray_step); }
                            
                        }
                    }
                }

                foreach (var pos in oArray)
                {
                    ///пройтись по направлению до конца луча длины {ray_len} или до другого символа
                    ///посчитать число элементов
                    ///прокрасить все поля от до весом удаляющимся от центра
                    ///важно что при стрике ход должен быть в приоритете

                    foreach (var vec in new List<Vector2>() { new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1), new Vector2(-1, 0) }){
                        int i;
                        for (i = 1; i < ray_len; i++){
                            if (xArray.Contains(pos + i * vec)){
                                i--;
                                break;
                            }
                        }
                        int force = 0;
                        for (; i > -ray_len; i--){
                            if (xArray.Contains(pos + i * vec)){
                                i++;
                                break;
                            }
                            if (oArray.Contains(pos + i * vec)) { force += ray_force; force *= ray_stack; }
                        }
                        for (; i < ray_len; i++){
                            if (oArray.Contains(pos + i * vec)) { continue; }
                            if (xArray.Contains(pos + i * vec)) { break; }
                            if (turn == "o"){
                                if (oCalculatedWeights.ContainsKey(pos + vec * i)) { oCalculatedWeights[pos + vec * i] += force - Math.Abs(i) * ray_step; }
                                else { oCalculatedWeights.Add(pos + vec * i, force - Math.Abs(i) * ray_step); }
                            }else{
                                if (oCalculatedWeights.ContainsKey(pos + vec * i)) { oCalculatedWeights[pos + vec * i] += force - Math.Abs(i) * ray_step; }
                                else { oCalculatedWeights.Add(pos + vec * i, force - Math.Abs(i) * ray_step); }
                            }
                        }
                    }
                }
            }



            //foreach (var pos in xArray){
            //    foreach (var vec in new List<Vector2>() { new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1), new Vector2(-1, 0) }){
            //        int max, min;
            //        int force = ray_force;
            //        for (max = 1; max<ray_len; max++){
            //            if (oArray.Contains(pos + max * vec)) {max = -max+1;break; }
            //            if (xArray.Contains(pos + max * vec)) { force += ray_force; force *= ray_stack; }
            //        }
            //        for (min = 1; max<ray_len; min++){
            //            if (oArray.Contains(pos - min * vec)) { min = -min +1;break; }
            //            if (xArray.Contains(pos - min * vec)) { force += ray_force; force *= ray_stack; }
            //        }
            //        if ((max <0 && min < 0) && (-(min+max)<5+1)){//с обоих сторон "o" и длина между ними меньше победной
            //            force = ray_force;
            //        }

            //        for (var i = -min; i <= max; i++){
            //            if (i == 0) { continue; }
            //            if (xArray.Contains(pos + i * vec)) { continue; }
            //            if (xCalculatedWeights.ContainsKey(pos + vec * i)) { xCalculatedWeights[pos + vec * i] += force - Math.Abs(i) * ray_step; }
            //            else { xCalculatedWeights.Add(pos + vec * i, force - Math.Abs(i) * ray_step); }
            //        }
            //    }
            //}

        }






        /*void Choose(string turn)
        {
            ///
            foreach
        }*/







    }

}