using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Kontur.LogPacker.Tests
{
    internal class LogUnpackerTests
    {
        [Test]
        public void ShouldUnpackSingleLine()
        {
            var source = @"2018-11-13 00:02:41,344 845    INFO  [0de1db] Doing some complicated stuff.. Random numbers are: 1192743271, 187325574, 168764164.";
            var packed = new LogPacker().PackLines(new [] {source});
            var result = new LogUnpacker().UnpackLines(packed).ToList();
            Assert.AreEqual(source, result.Single());
        }

        [Test]
        public void ShouldUnpackTwoLines()
        {
            var s1 = "2018-11-13 00:02:41,475 976    INFO  [8afb16] Doing some complicated stuff.. Random numbers are: 144508255, 619263569, 1986743925.";
            var s2 = "2018-11-13 00:02:41,477 978    INFO  [8afb16] Here's some useful guids:";
            var packed = new LogPacker().PackLines(new[] { s1, s2 }).ToList();
            var result = new LogUnpacker().UnpackLines(packed).ToList();
            CollectionAssert.AreEqual(new List<string> {s1, s2}, result);
        }

        [Test]
        public void ShouldUnpackNonLogLine()
        {
            var source = "59eb3535 - 733c - 4c2f - 8c02 - 7512aa3403b1";
            var packed = new LogPacker().PackLines(new[] { source });
            var result = new LogUnpacker().UnpackLines(packed).ToList();
            Assert.AreEqual(source, result.Single());
        }

        [Test]
        public void ShouldUnpackBinaryLine()
        {
            var source =
                @"aЬЯґЊI&№)BЖхк\№гБя %Бхv?­sЦцвcу8AкkєKЮ<Имtv>¶ЅPVьdц5›в
ыц;q1—|{тWИю•r¤”9“&5—+2ЕHµ‰dи8«;U	ќ†ҐЫд1щeB?їТєЋЙ‘ђvR~б„ю""DHыЉ­ < G9ЗRZ™TЗуf¬GwЁH«“6j = јаvЄ°®^·$sWЃSњbґЋД±™ЪГЏ—±Ў¦R3ћJw2аюр“\¶Ї¤н -= аэдѕ•…hsАЦЌNI
жЏ”ѓљћПHФµ)|»; ;‹‡л¶(ЛТёZ; Ь®jьAMћ#¶ЯО№–\Z)я jµ›ч™Й…ие†±g‚Ff_з]'Дя K3ЬЫя мЖ®?Вdїв#‘""Ц&…„©ђьХBfћџ7‘}№ЖЧъWcvїЕЮєhїuЈ›‹ еRJјVЭКЎ2хvЪ Ач»сЪ”V Л2ЕтрЅЄ™Џ
Ќ§љ¦Ѓ-гb@ЕNШUЛ |«њdњS[R®бЋ <№ЏDИp¤ФJEEжs’М
ЄМ]I4ЇЎ - jVу""ЂпќСF{ЧOoс;OТnн¬ж»8Ъэ‹Ы¦AфйЉЖucµХќ4©ОM>‡сзД’я јЯО№6ђґjаЖ¦’c†ДъlЙo©[M!ВG""іv·| cЄЪjЧЦТЩЛж""CµѕR0r}j”—іqOќ3ќ*ЦHІt©“­Q,ІЈ5ЬБ8№Уasђ3[С{Ј*ѓб89=;T¬ЫЖє3#	ѓЧ©лW­€SBe©И?ќT +TБЅвЩЫI9Q„qФЧ'q«ЬЭЬ&o Ѓф¬jОЦFђЏPієeЋ]·Я'SЗ©¬во[%x•ЙЭРW;Ёкй46ыN8/юкФдЏQҐн'®ИБ’V•·3'№5EyъіТС+#в0ЕЃэцюfёъйџДpCбBУ…ACЕHўљb1V#512АWUЎй}\Џ§щНmKв3ћЖњpџКћWj“Ћ+Ґ#""6Й©Ые#µ!љ0ї›W<цЁdЏЋ•d™єь›mв€wЛџе\Є‘»9®JПЮ6†ЕќШ
r9­»'V…HўЋ[ъЭй‚КM§ОБЏzгdeф¬1
№ШкВ«AІ - э©¤“X¤k & tЯЇ‰¦П]НџМЧ]> &qSшE§";
            var packed = new LogPacker().PackLines(new[] { source });
            var result = new LogUnpacker().UnpackLines(packed).ToList();
            Assert.AreEqual(source, result.Single());
        }
    }
}
