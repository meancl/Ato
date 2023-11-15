using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtoReplayer.Models
{
    class ArrowColors
    {
        public const int FAKE_REQUEST_SIGNAL = -1;
        public const int FAKE_BUY_SIGNAL = 0; // 주황색 페이크 매수
        public const int FAKE_RESIST_SIGNAL = 1; // 초록색 페이크 저항
        public const int FAKE_ASSISTANT_SIGNAL = 2; // 노랑색 페이크 보조
        public const int FAKE_VOLATILE_SIGNAL = 7; // 남색 페이크 변동성
        public const int EVERY_SIGNAL = 8;
        public const int FAKE_DOWN_SIGNAL = 9; // 보라색 페이크 다운
        public const int PAPER_BUY_SIGNAL = 10; // 빨강색 모의매수
        public const int PAPER_SELL_SIGNAL = 11; // 파랑색 모의매도
    }
}
