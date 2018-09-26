using System.Collections.Generic;
using System.Threading.Tasks;
using Cynthia.Card;

namespace Cynthia.Card
{
    public interface IGwentServerGame
    {
        Player[] Players { get; set; }//玩家数据传输/
        bool[] IsPlayersLeader { get; set; }//玩家领袖是否可用/
        IList<GameCard>[] PlayersLeader { get; set; }//玩家领袖是?/
        TwoPlayer GameRound { get; set; }//谁的的回合----
        int RoundCount { get; set; }//有效比分的回合数
        int CurrentRoundCount { get; set; }//当前小局
        int[] PlayersWinCount { get; set; }//玩家胜利场数/
        int[][] PlayersRoundResult { get; set; }//三局r1 r2 r3
        IList<GameCard>[] PlayersDeck { get; set; }//玩家卡组/
        IList<GameCard>[] PlayersHandCard { get; set; }//玩家手牌/
        IList<GameCard>[][] PlayersPlace { get; set; }//玩家场地/
        IList<GameCard>[] PlayersCemetery { get; set; }//玩家墓地/
        IList<GameCard>[] PlayersStay { get; set; }//玩家悬牌
        Faction[] PlayersFaction { get; set; }//玩家们的势力
        bool[] IsPlayersPass { get; set; }//玩家是否已经pass
        bool[] IsPlayersMulligan { get; set; }//玩家是否调度完毕
        int Player1Index { get; }//玩家1的坐标
        int Player2Index { get; }//玩家2的坐标
        Task<bool> Play();
        Task<bool> PlayerRound();
        Task RoundPlayCard(int playerIndex, RoundInfo cardInfo);//哪一位玩家,打出第几张手牌,打到了第几排,第几列
        void LogicDrawCard(int playerIndex, int count);//或许应该播放抽卡动画和更新数值
        //封装的抽卡
        Task DrawCard(int player1Count, int player2Count);
        //封装的调度
        Task MulliganCard(int playerIndex, int count);
        Task DrawCardAnimation(int myPlayerIndex, int myPlayerCount, int enemyPlayerIndex, int enemyPlayerCount);
        //-------------------------------------------------------------------------------------------------------------------------
        //下面是发送数据包,或者进行一些初始化信息
        //根据当前信息,处理游戏结果
        //将某个列表中的元素,移动到另一个列表的某个位置,然后返回被移动的元素     
        GameCard LogicCardMove(IList<GameCard> soure, int soureIndex, IList<GameCard> taget, int tagetIndex);
        Task GameOverExecute();
        //----------------------------------------------------------------------------------------------
        //自动向玩家推送更新消息
        Task SetAllInfo();
        Task SetCemeteryInfo(int playerIndex);
        Task SetGameInfo();
        Task SetCardsInfo();
        Task SetPointInfo();
        Task SetCountInfo();
        Task SetPassInfo();
        Task SetMulliganInfo();
        Task SetWinCountInfo();
        Task SetNameInfo();
        //---------------------------------------------------------------------------------------------
        //Task GetCardFrom(int playerIndex, RowPosition getPosition, RowPosition taget, int index, CardStatus cardInfo);
        //Task SetCardTo(int playerIndex, RowPosition rowIndex, int cardIndex, RowPosition tagetRowIndex, int tagetCardIndex);
        Task CardOn(int playerIndex, CardLocation location);
        Task CardDown(int playerIndex, CardLocation location);
        Task CardMove(int playerIndex, MoveCardInfo info);
        //----------------------------------------------------------------------------------------------
        Task SendGameResult(TwoPlayer player);
        void ToCemeteryInfo(GameCard card);
        Task SendBigRoundEndToCemetery();
        RowPosition ListToRow(int myPlayerIndex, IList<GameCard> list);
        IList<GameCard> RowToList(int myPlayerIndex, RowPosition row);
        int AnotherPlayer(int playerIndex);
        CardLocation GetCardLocation(int playerIndex, RowPosition row, GameCard card);
    }
}