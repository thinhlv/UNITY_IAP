#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("25EeIdKcKq5hHxAAfzxQYk4fuBIY2hqKdlz3j0yO4sDYrQRxAyCBXycdi4XCTWvxoH8dXG/oqh1l1+qAgAMNAjKAAwgAgAMDAo34JFNp9+VAtufXajfWobuUgn67VqQAVzQrNoLNuMKnFZbLSwTWCaDVB6Jpact4bXjFwl/mOKydZHJvG/5m1kOUjB6Nux4lkRXqZiW7jEt9OseLET/oJovWbXhBiD7le9zVAjrTsaN9R6j1dtVobh7C29ChCFlHpfVfqvPeNjd+MJ9dzuuzWAMflsDqdb0vIZ8ZA8Nb76ocuoQrsaul/zKvjVVPW7SVMoADIDIPBAsohEqE9Q8DAwMHAgEf6De5jJFJzkSd9i291TQZQKWngKqg9zd/aJMhDwABAwID");
        private static int[] order = new int[] { 10,7,3,7,11,13,13,8,8,12,11,13,13,13,14 };
        private static int key = 2;

        public static byte[] Data() {
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
