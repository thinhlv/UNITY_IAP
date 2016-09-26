#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("wY77geRW1YgIR5VK45ZE4SoqiDvDQE5BccNAS0PDQEBBzrtnECq0pmReyMaBDiiy4zxeHyyr6V4mlKnDXKt0+s/SCo0H3rVu/pZ3WgPm5MPO+F1m0lapJWb4zwg+eYTIUnyrZT1z3B6NqPAbQFzVg6k2/mxi3FpAmNJdYpHfae0iXFNDPH8TIQ1c+1HIlS47Ast9pjiflkF5kPLgPgTrtgP1pJQpdJXi+NfBPfgV50MUd2h1W5lZyTUftMwPzaGDm+5HMkBjwhxxw0BjcUxHSGvHCce2TEBAQERBQjWWKy1dgZiT4ksaBOa2HOmwnXV0gBis6V/5x2jy6Oa8cezOFgwY99YuO4aBHKV7794nMSxYvSWVANfPXenjtHQ8K9BiTENCQEFA");
        private static int[] order = new int[] { 13,1,3,9,8,8,10,9,11,9,13,12,13,13,14 };
        private static int key = 65;

        public static byte[] Data() {
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
