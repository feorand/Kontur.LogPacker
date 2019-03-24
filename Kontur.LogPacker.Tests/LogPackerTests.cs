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
@"a�ߴ�I&�)B���\���� %��v?�s���c�8A�k�K�<��tv>��PV�d�5��
��;q1�|{�W���r��9�&5�+2�H��d�8�;U	�����1�eB?�Һ����vR~��""DH��� < G9�RZ�T��f�Gw�H���6j = ��v���^�$sW�S�b��ı��Ï����R3�Jw2���\���� -= ������hs�֍NI�
揔����HԵ)|�; ;���(�ҸZ; ܮj�AM�#���ι�\Z)� j�������冱g�Ff_�]'�� K3��� �Ʈ?�d��#�""�&������Bf��7�}����Wcv��޺h�u��� �RJ�V�ʡ2�v� ����ڔV��2��𽪙�
�����-�b@�N�U� |��d�S[R�� <��D�p��JEE�s��
��]I4�� - jV�""���F{�Oo�;O�n��8���ۦA���uc�՝4��M>���Ē� ��ι6��j��Ʀ�c���l�o�[M!�G""�v�| c��j������""C��R0r}j���qO�3�*�H�t���Q,��5��8��a�s�3[�{�*��89=;T����3#	�ש�W��SBe��?�T +T�����I9Q�q��'q����&o����j��F��P��e�]��'Sǩ���[%x����W;���46�N8/���䏘Q��'����V��3'�5Ey����+#�0�����f����pC�BӅAC�H��b1V#512�WU��}\����mK�3�Ɯp�ʞWj��+�#""6����#�!�0��W<��d���d����m�w˟�\���9�J��6�ŝ�
r9��'V�H��[����M����z�de��1
���«A� - ����X�k & t�����]͟��]> &qS�E�";
            var result = new LogPacker().PackLines(new[] { s1 }).ToList();
            Assert.AreEqual(5, result.Count);
        }
    }
}
