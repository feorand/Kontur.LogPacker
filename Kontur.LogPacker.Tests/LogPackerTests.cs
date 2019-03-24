using System.Linq;
using NUnit.Framework;

namespace Kontur.LogPacker.Tests
{
    public class LogPackerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldPackSingleLine()
        {
            var source =
                @"2018-11-13 00:02:41,344 845    INFO  [0de1db] Doing some complicated stuff.. Random numbers are: 1192743271, 187325574, 168764164.";
            var result = new LogPacker().PackLines(new[] {source}).ToList();
            Assert.AreEqual(19, result.Count);
        }

        [Test]
        public void ShouldPackTwoLines()
        {
            var s1 = "2018-11-13 00:02:41,475 976    INFO  [8afb16] Doing some complicated stuff.. Random numbers are: 144508255, 619263569, 1986743925.";
            var s2 = "2018-11-13 00:02:41,477 978    INFO  [8afb16] Here's some useful guids:";
            var result = new LogPacker().PackLines(new[] { s1, s2 }).ToList();
            Assert.AreEqual(22, result.Count);
        }

        [Test]
        public void ShouldNotPackNonLogTextLine()
        {
            var s1 = "59eb3535 - 733c - 4c2f - 8c02 - 7512aa3403b1";
            var result = new LogPacker().PackLines(new[] { s1 }).ToList();
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void ShouldNotPackBinaryLine()
        {
            var s1 = 
@"aÜß´ŒI&¹)BÆõê\¹ãÁÿ %Áõv?­sÖöâcó8AêkºKŞ<Èìtv>¶½PVüdö5›â
ûö;q1—|{òWÈş•r¤”9“&5—+2ÅHµ‰dè8«;U	†¥Ûä1ùeB?¿ÒºÉ‘vR~á„ş""DHûŠ­ < G9ÇRZ™TÇóf¬Gw¨H«“˜6j = ¼àvª°®^·$sWSœb´Ä±™ÚÃ—±¡¦R3Jw2àşğ“\¶¯¤í -= àıä¾•…hsÀÖNI˜
æ”ƒšÏHÔµ)|»; ;‹‡ë¶(ËÒ¸Z; Ü®jüAM#˜¶ßÎ¹–\Z)ÿ jµ›÷™É…èå†±g‚Ff_ç]'Äÿ K3ÜÛÿ ìÆ®?Âd¿â#‘""Ö&…„©üÕBfŸ7‘}¹Æ×úWcv¿ÅŞºh¿u£›‹ åRJ¼VİÊ¡2õvÚ À÷»ñÚ”V Ë2Åòğ½ª™
§š¦-ãb@ÅNØUË |«œdœS[R®á <¹DÈp¤ÔJEEæs’Ì
ªÌ]I4¯¡ - jVó""€ïÑF{×Ooñ;OÒní¬æ»8Úı‹Û¦AôéŠÆucµÕ4©ÎM>‡ñçÄ’ÿ ¼ßÎ¹6´j˜àÆ¦’c†ÄúlÉo©[M!ÂG""³v·| cªÚj×ÖÒÙËæ""Cµ¾R0r}j”—³qO3*ÖH²t©“­Q,²£5ÜÁ8¹Óa˜s3[Ñ{£*ƒá89=;T¬ÛÆº3#	ƒ×©ëW­ˆSBe©È?T +TÁ½âÙÛI9Q„qÔ×'q«ÜİÜ&o ô¬jÎÖFP³ºe]·ß'SÇ©¬âî[%x•ÉİĞW;¨êé46ûN8/şêÔä˜Q¥í'®ÈÁ’V•·3'¹5Eyú³ÒÑ+#â0Åıöşf¸úéŸÄpCáBÓ…ACÅH¢šb1V#512ÀWU¡é}\§ùÍmKâ3ÆœpŸÊWj“+¥#""6É©Ûå#µ!š0¿›W<ö¨d•d™ºü›mâˆwËŸå\ª‘»9®JÏŞ6†ÅØ
r9­»'V…H¢[úİé‚ÊM§ÎÁzãdeô¬1
¹ØêÂ«A² - ı©¤“X¤k & tß¯‰¦Ï]ÍŸÌ×]> &qSøE§";
            var result = new LogPacker().PackLines(new[] { s1 }).ToList();
            Assert.AreEqual(5, result.Count);
        }
    }
}
