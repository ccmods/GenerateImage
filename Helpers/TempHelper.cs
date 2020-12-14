namespace Helpers {
    public class TempHelper {
        public static double kToF(double kVal) {
            double tmp = (kVal*9)/5;
            return tmp-459.67;
        }
    }
}