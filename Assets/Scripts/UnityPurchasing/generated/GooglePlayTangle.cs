// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("B4SKhbUHhI+HB4SEhQ992WtKPin3s98EK3Q+JrGzrj9ztYVZRxYPH3zDowbBouLplFK/6dw5OxiTns5OCkvx0amHgPmM3kfIfqbNpbccwzuetk7XXtGTG04VJ6zoiMpGWnY04mJzpOOLNS7hGAiccEcu3qUJBo0zLoFyrKfpLyVXWhmJQoNxmc0X+K1dC7E9kmBN2WKzqda1Ew19TkEEOEwWbK404o4sie+cZkgjM41AKjrLHMbseoizUREfWXeCAolmAtmFvVLB56kwHsyWN7yG7AX+Ps+8PvQEVLUHhKe1iIOMrwPNA3KIhISEgIWGEy1R1IAEzEg/DQPmNwPmX/Pz4Yax2q/4+yKl96cfFXlqwnwWHOQiwMASmItxa2YxNoeGhIWE");
        private static int[] order = new int[] { 1,4,9,3,11,9,7,8,12,12,12,11,13,13,14 };
        private static int key = 133;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
