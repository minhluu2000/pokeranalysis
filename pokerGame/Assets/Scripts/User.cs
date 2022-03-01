using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using TMPro;

public class User : MonoBehaviour
{
    public static User Instance;

    private const byte GET_CARDS = 0;
    private const byte CHECK = 1;
    private const byte FOLD = 2;
    private const byte CALL = 3;
    private const byte BET = 4;
    private const byte RAISE = 5;
    private const byte SMALL_BLIND = 6;
    private const byte BIG_BLIND = 7;
    private const byte UPDATE_HOST_MONEY = 8;
    private const byte UPDATE_SAVED_MONEY = 9;
    private const byte UPDATE_ALL_MONEY = 10;
    private const byte ACTIVATE_TURN = 11;
    private const byte START_GAME = 12;
    private const byte FLOP = 13;
    private const byte RIVER = 14;
    private const byte TURN = 15;
    private const byte START_ROUND = 16;
    private const byte START_POKER_GAME = 17;
    private const byte WINNER_IN_ROUND = 18;
    private const byte MULTIPLE_WINNER = 19;
    private const byte PLAYER_FOLDED = 20;
    private const byte ADD_MONEY_TO_PLAYER = 21;
    private const byte SHOW_ALL_CARDS = 22;
    private const byte WAIT_START = 23;
    int money;
    int playerPosition;
    int numPlayers;
    int dealer;
    int moneyUpdates;
    int pot;
    int betAtPlayer;
    int moneyAlreadyBet;
    int totalBet;
    int playersInRound;
    int checksInARow;
    int callsInARow;
    int round;
    int[] cardNumbers;
    bool inRound;
    bool someoneBet;
    string JSONoutput;
    string JSONinput;
    string NickInput;
    GameObject Card;
    List <GameObject> cardDeck = new List<GameObject>();
    List <GameObject> Cards = new List<GameObject>();
    List <GameObject> CardsInstan = new List<GameObject>();
    List <int> numberDeck = new List <int>();
    List <int> cardNumbersList = new List<int>();
    List <int> allMoney = new List<int>();
    List <int> playersInRoundArray = new List<int>();
    [SerializeField] TMP_Text PotArea;
    [SerializeField] TMP_Text BetArea;
    [SerializeField] TMP_Text[] AllMoney;
    [SerializeField] TMP_Text[] AllNames;
    [SerializeField] TMP_InputField MoneyInput;
    public GameObject[] Areas;
    public GameObject MiddleArea;
    public GameObject CardBack;
    public GameObject CheckButton;
    public GameObject FoldButton;
    public GameObject CallButton;
    public GameObject BetButton;
    public GameObject RaiseButton;
    public GameObject TwoH;
    public GameObject TwoD;
    public GameObject TwoC;
    public GameObject TwoS;
    public GameObject ThreeH;
    public GameObject ThreeD;
    public GameObject ThreeC;
    public GameObject ThreeS;
    public GameObject FourH;
    public GameObject FourD;
    public GameObject FourC;
    public GameObject FourS;
    public GameObject FiveH;
    public GameObject FiveD;
    public GameObject FiveC;
    public GameObject FiveS;
    public GameObject SixH;
    public GameObject SixD;
    public GameObject SixC;
    public GameObject SixS;
    public GameObject SevenH;
    public GameObject SevenD;
    public GameObject SevenC;
    public GameObject SevenS;
    public GameObject EightH;
    public GameObject EightD;
    public GameObject EightC;
    public GameObject EightS;
    public GameObject NineH;
    public GameObject NineD;
    public GameObject NineC;
    public GameObject NineS;
    public GameObject TenH;
    public GameObject TenD;
    public GameObject TenC;
    public GameObject TenS;
    public GameObject JH;
    public GameObject JD;
    public GameObject JC;
    public GameObject JS;
    public GameObject QH;
    public GameObject QD;
    public GameObject QC;
    public GameObject QS;
    public GameObject KH;
    public GameObject KD;
    public GameObject KC;
    public GameObject KS;
    public GameObject AH;
    public GameObject AD;
    public GameObject AC;
    public GameObject AS;
    RaiseEventOptions raiseEventOptions = new RaiseEventOptions {Receivers = ReceiverGroup.All};
    
    void makeDeck(){
        numberDeck.Clear();
        cardDeck.Clear();
        for (int i = 0; i < 52; ++i){
            numberDeck.Add(i);
        }
        cardDeck.Add(TwoH);
        cardDeck.Add(TwoD);
        cardDeck.Add(TwoC);
        cardDeck.Add(TwoS);
        cardDeck.Add(ThreeH);
        cardDeck.Add(ThreeD);
        cardDeck.Add(ThreeC);
        cardDeck.Add(ThreeS);
        cardDeck.Add(FourH);
        cardDeck.Add(FourD);
        cardDeck.Add(FourC);
        cardDeck.Add(FourS);
        cardDeck.Add(FiveH);
        cardDeck.Add(FiveD);
        cardDeck.Add(FiveC);
        cardDeck.Add(FiveS);
        cardDeck.Add(SixH);
        cardDeck.Add(SixD);
        cardDeck.Add(SixC);
        cardDeck.Add(SixS);
        cardDeck.Add(SevenH);
        cardDeck.Add(SevenD);
        cardDeck.Add(SevenC);
        cardDeck.Add(SevenS);
        cardDeck.Add(EightH);
        cardDeck.Add(EightD);
        cardDeck.Add(EightC);
        cardDeck.Add(EightS);
        cardDeck.Add(NineH);
        cardDeck.Add(NineD);
        cardDeck.Add(NineC);
        cardDeck.Add(NineS);
        cardDeck.Add(TenH);
        cardDeck.Add(TenD);
        cardDeck.Add(TenC);
        cardDeck.Add(TenS);
        cardDeck.Add(JH);
        cardDeck.Add(JD);
        cardDeck.Add(JC);
        cardDeck.Add(JS);
        cardDeck.Add(QH);
        cardDeck.Add(QD);
        cardDeck.Add(QC);
        cardDeck.Add(QS);
        cardDeck.Add(KH);
        cardDeck.Add(KD);
        cardDeck.Add(KC);
        cardDeck.Add(KS);
        cardDeck.Add(AH);
        cardDeck.Add(AD);
        cardDeck.Add(AC);
        cardDeck.Add(AS);
    }    
 
    public void leave(){
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(0);
    }

    public void call(){
        if(money >= betAtPlayer){
            money -= betAtPlayer;
            moneyAlreadyBet += betAtPlayer;
            endTurn();
            object[] data = new object[] {(playerPosition + 1) % numPlayers, betAtPlayer};
            PhotonNetwork.RaiseEvent(CALL, data, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    public void bet(){
        int moneyToBet = int.Parse(MoneyInput.text);
        if(money >= moneyToBet && money > 0){
            MoneyInput.text = "";
            money -= moneyToBet;
            moneyAlreadyBet += moneyToBet;
            endTurn();
            object[] data = new object[] {(playerPosition + 1) % numPlayers, moneyToBet};
            PhotonNetwork.RaiseEvent(BET, data, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    public void raise(){
        int moneyToBet = int.Parse(MoneyInput.text);
        if(money >= moneyToBet + betAtPlayer && money > 0){
            MoneyInput.text = "";
            money -= moneyToBet + betAtPlayer;
            moneyAlreadyBet += moneyToBet + betAtPlayer;
            endTurn();
            object[] data = new object[] {(playerPosition + 1) % numPlayers, moneyToBet, betAtPlayer};
            PhotonNetwork.RaiseEvent(RAISE, data, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    public void check(){
        endTurn();
        object[] data = new object[] {(playerPosition + 1) % numPlayers};
        PhotonNetwork.RaiseEvent(CHECK, data, raiseEventOptions, SendOptions.SendReliable);
    }

    public void fold(){
        inRound = false;
        endTurn();
        object[] data = new object[] {playerPosition};
        PhotonNetwork.RaiseEvent(PLAYER_FOLDED, data, raiseEventOptions, SendOptions.SendReliable);
        PhotonNetwork.RaiseEvent(FOLD, data, raiseEventOptions, SendOptions.SendReliable);
    }

    void OnEnable() {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    void OnDisable() {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    void NetworkingClient_EventReceived(EventData obj){
        byte code = obj.Code;
        object[] data = (object[])obj.CustomData;
        if (code == START_GAME){
            startGame();
        }else if (code == GET_CARDS){
            int [] temp = (int[]) data[0];
            Cards.Clear();
            for (int i = 0; i < temp.Length; ++i){
                Cards.Add(cardDeck[temp[i]]);
            }
            getCards();
            data = null;
        }else if(code == CHECK){
            checksInARow += 1;
            int temp = (int)data[0];
            if (playerPosition == temp){
                activateTurn(temp, false);
            }
        }else if(code == FOLD){
            playersInRound -= 1;
            int temp1 = (int)data[0];
            if (playerPosition > temp1){
                Areas[numPlayers - (playerPosition - temp1)].SetActive(false);
            }else {
                Areas[temp1 - playerPosition].SetActive(false);
            }
            if (playerPosition == (temp1 + 1) % numPlayers){
                activateTurn(playerPosition, false);
            }
        }else if(code == CALL){
            callsInARow += 1;
            int temp1 = (int)data[0];
            int temp2 = (int)data[1];
            pot += temp2;
            if (playerPosition == temp1){
                activateTurn(temp1, false);
            }
        }else if(code == BET){
            someoneBet = true;
            callsInARow = 0;
            int temp1 = (int)data[0];
            int temp2 = (int)data[1];
            pot += temp2;
            totalBet += temp2;
            if (playerPosition == temp1){
                activateTurn(temp1, true);
            }
        }else if(code == RAISE){
            someoneBet = true;
            callsInARow = 0;
            int temp1 = (int)data[0];
            int temp2 = (int)data[1];
            int temp3 = (int)data[2];
            pot += temp2 + temp3;
            totalBet += temp2;
            if (playerPosition == temp1){
                activateTurn(temp1, true);
            }
        }else if(code == SMALL_BLIND){
            int temp = (int) data[0];
            StartCoroutine(smallBlind(temp));
        }else if(code == BIG_BLIND){
            int temp = (int) data[0];
            bigBlind(temp);
        }else if(code == UPDATE_HOST_MONEY){
            updateHostMoney();
        }else if(code == UPDATE_SAVED_MONEY){
            if(PhotonNetwork.IsMasterClient){
                int temp1 = (int) data[0];
                int temp2 = (int) data[1];
                updateSavedMoney(temp1, temp2);
            }
        }else if(code == UPDATE_ALL_MONEY){
            int[] temp1 = (int[]) data[0];
            int temp2 = (int) data[1];
            int temp3 = (int) data[2];
            updateAllMoney(temp1, temp2, temp3);
        }else if(code == ACTIVATE_TURN){
            int temp1 = (int) data[0];
            bool temp2 = (bool) data[1];
            activateTurn(temp1, temp2);
        }else if(code == FLOP){
            flop();
        }else if(code == RIVER){
            river();
        }else if(code == TURN){
            turn();
        }else if(code == START_ROUND){
            startRound();
        }else if(code == START_POKER_GAME){
            if (PhotonNetwork.IsMasterClient){
                StartCoroutine(startPokerGame());
            }
        }else if(code == WINNER_IN_ROUND){
            waitWinnerInRound();
        }else if(code == MULTIPLE_WINNER){
            if(PhotonNetwork.IsMasterClient){
                JSONinput += "cc=" + Cards[numPlayers * 2].name + "," + Cards[numPlayers * 2 + 1].name + "," + Cards[numPlayers * 2 + 2].name + "," + Cards[numPlayers * 2 + 3].name + "," + Cards[numPlayers * 2 + 4].name;
                for (int i = 0; i < playersInRoundArray.Count; ++i){
                    Debug.Log("array " + i + " is " + playersInRoundArray[i]);
                    if (playersInRoundArray[i] == 1){
                        JSONinput += "&pc[]=" + Cards[i * 2].name + "," + Cards[i * 2 + 1].name;
                    }
                }
                Debug.Log(JSONinput);
                StartCoroutine(GetRequest(JSONinput));
            }
        }else if(code == PLAYER_FOLDED){
            if (PhotonNetwork.IsMasterClient){
                int temp = (int)data[0];
                playerFolded(temp);
            }
        }else if(code == ADD_MONEY_TO_PLAYER){
            int temp1 = (int) data[0];
            int temp2 = (int) data[1];
            if (temp1 == playerPosition){
                money += temp2;
            }
        }else if(code == SHOW_ALL_CARDS){
            for (int i = 0; i < playersInRoundArray.Count; ++i){
                if (playersInRoundArray[i] == 1){
                    Destroy(Areas[i].transform.GetChild(0).gameObject);
                    Destroy(Areas[i].transform.GetChild(1).gameObject);
                    CardsInstan.Add(Instantiate(Cards[(i * 2 + playerPosition * 2) % (numPlayers * 2)], new Vector3(0, 0, 0), Quaternion.identity));
                    CardsInstan.Add(Instantiate(Cards[(i * 2 + playerPosition * 2 + 1) % (numPlayers * 2)], new Vector3(0, 0, 0), Quaternion.identity));
                    CardsInstan[CardsInstan.Count - 2].transform.SetParent(Areas[i].transform, false);
                    CardsInstan[CardsInstan.Count - 1].transform.SetParent(Areas[i].transform, false);
                }
            }
            PhotonNetwork.RaiseEvent(WAIT_START, null, raiseEventOptions, SendOptions.SendReliable);
        }else if(code == WAIT_START){
            if (PhotonNetwork.IsMasterClient){
                StartCoroutine(waitBeforeStartGame());
            }
        }
    }

    GameObject getRandomCard(){
        int randomNumber = Random.Range(0, numberDeck.Count-1);
        cardNumbersList.Add(numberDeck[randomNumber]);
        GameObject randomCard = cardDeck[numberDeck[randomNumber]];
        numberDeck.RemoveAt(randomNumber);
        return randomCard;
    }

    void getCards(){
        CardsInstan = new List<GameObject>();
        for (int i = 0; i < numPlayers * 2; ++i){
            if (i < 2){
                CardsInstan.Add(Instantiate(Cards[(i + playerPosition*2) % (numPlayers * 2)], new Vector3(0, 0, 0), Quaternion.identity));
            }else{
                CardsInstan.Add(Instantiate(CardBack, new Vector3(0, 0, 0), Quaternion.identity));
            }
            CardsInstan[i].transform.SetParent(Areas[i/2].transform, false);
        }
    }

    IEnumerator smallBlind(int position){
        if(playerPosition == position){
            money -= 10;
            moneyAlreadyBet += 10;
            yield return new WaitForSeconds(2);
            PhotonNetwork.RaiseEvent(UPDATE_HOST_MONEY, null, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    void bigBlind(int position){
        if(playerPosition == position){
            money -= 20;
            moneyAlreadyBet += 20;
        }
    }

    void updateHostMoney(){
        object[] data = new object[]{playerPosition, money};
        PhotonNetwork.RaiseEvent(UPDATE_SAVED_MONEY, data, raiseEventOptions, SendOptions.SendReliable);
        data = null;
        Debug.Log("Update Host Money");
    }

    void updateSavedMoney(int position, int value){
        allMoney[position] = value;
        moneyUpdates += 1;
        if(moneyUpdates == numPlayers){
            object[] data = new object[]{allMoney.ToArray(), pot, totalBet};
            PhotonNetwork.RaiseEvent(UPDATE_ALL_MONEY, data, raiseEventOptions, SendOptions.SendReliable);
            data = null;
            moneyUpdates = 0;
        }
        Debug.Log("Update Saved Money");
    }

    void updateAllMoney(int[] allPlayerMoney, int moneyPot, int moneyBet){
        for(int i = 0; i < numPlayers; ++i){
            AllMoney[i].text = "$" + allPlayerMoney[(i + playerPosition) % numPlayers].ToString();
        }
        betAtPlayer = moneyBet - moneyAlreadyBet;
        BetArea.text = "$" + (betAtPlayer).ToString();
        PotArea.text = "$" + moneyPot.ToString();
        pot = moneyPot;
        Debug.Log("Update All Money");
    }

    void winner(){
        money += pot;
        PhotonNetwork.RaiseEvent(UPDATE_HOST_MONEY, null, raiseEventOptions, SendOptions.SendReliable);
        PhotonNetwork.RaiseEvent(WAIT_START, null, raiseEventOptions, SendOptions.SendReliable);
    }

    void activateTurn(int position, bool prevPersonBet){
        if (playersInRound == 1){
            PhotonNetwork.RaiseEvent(WINNER_IN_ROUND, null, raiseEventOptions, SendOptions.SendReliable);
            return;
        }

        if (round == 0 && (checksInARow == 1 || (someoneBet && callsInARow == playersInRound - 1))){
            PhotonNetwork.RaiseEvent(START_ROUND, null, raiseEventOptions, SendOptions.SendReliable);
            PhotonNetwork.RaiseEvent(FLOP, null, raiseEventOptions, SendOptions.SendReliable);
            return;
        }else if (round == 1 && (callsInARow == playersInRound - 1 || checksInARow == playersInRound)){
            PhotonNetwork.RaiseEvent(START_ROUND, null, raiseEventOptions, SendOptions.SendReliable);
            PhotonNetwork.RaiseEvent(RIVER, null, raiseEventOptions, SendOptions.SendReliable);
            return;
        }else if (round == 2 && (callsInARow == playersInRound -1 || checksInARow == playersInRound)){
            PhotonNetwork.RaiseEvent(START_ROUND, null, raiseEventOptions, SendOptions.SendReliable);
            PhotonNetwork.RaiseEvent(TURN, null, raiseEventOptions, SendOptions.SendReliable);
            return;
        }else if (round == 3 && (callsInARow == playersInRound - 1 || checksInARow == playersInRound)){
            PhotonNetwork.RaiseEvent(MULTIPLE_WINNER, null, raiseEventOptions, SendOptions.SendReliable);
            return;
        }

        if(playerPosition == position){
            PhotonNetwork.RaiseEvent(UPDATE_HOST_MONEY, null, raiseEventOptions, SendOptions.SendReliable);
            if (!inRound){
                object[] data = new object[] {(position + 1) % numPlayers, prevPersonBet};
                PhotonNetwork.RaiseEvent(ACTIVATE_TURN, data, raiseEventOptions, SendOptions.SendReliable);
                return;
            }
            FoldButton.SetActive(true);
            if (prevPersonBet){
                CallButton.SetActive(true);
                RaiseButton.SetActive(true);
            }else{
                CheckButton.SetActive(betAtPlayer == 0);
                CallButton.SetActive(betAtPlayer != 0);
                BetButton.SetActive(betAtPlayer == 0);
                RaiseButton.SetActive(betAtPlayer != 0);
            }
        }
    }

    void flop(){
        CardsInstan.Add(Instantiate(Cards[numPlayers * 2], new Vector3(0, 0, 0), Quaternion.identity));
        CardsInstan[numPlayers * 2].transform.SetParent(MiddleArea.transform, false);
        CardsInstan.Add(Instantiate(Cards[numPlayers * 2 + 1], new Vector3(0, 0, 0), Quaternion.identity));
        CardsInstan[numPlayers * 2 + 1].transform.SetParent(MiddleArea.transform, false);
        CardsInstan.Add(Instantiate(Cards[numPlayers * 2 + 2], new Vector3(0, 0, 0), Quaternion.identity));
        CardsInstan[numPlayers * 2 + 2].transform.SetParent(MiddleArea.transform, false);
        if (PhotonNetwork.IsMasterClient){
            object[] data = new object[] {(dealer + 1) % numPlayers, false};
            PhotonNetwork.RaiseEvent(ACTIVATE_TURN, data, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    void river(){
        CardsInstan.Add(Instantiate(Cards[(numPlayers * 2 + 3)], new Vector3(0, 0, 0), Quaternion.identity));
        CardsInstan[numPlayers * 2 + 3].transform.SetParent(MiddleArea.transform, false);
        if (PhotonNetwork.IsMasterClient){
            object[] data = new object[] {(dealer + 1) % numPlayers, false};
            PhotonNetwork.RaiseEvent(ACTIVATE_TURN, data, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    void turn(){
        CardsInstan.Add(Instantiate(Cards[(numPlayers * 2 + 4)], new Vector3(0, 0, 0), Quaternion.identity));
        CardsInstan[numPlayers * 2 + 4].transform.SetParent(MiddleArea.transform, false);
        if (PhotonNetwork.IsMasterClient){
            object[] data = new object[] {(dealer + 1) % numPlayers, false};
            PhotonNetwork.RaiseEvent(ACTIVATE_TURN, data, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    void endTurn(){
        CheckButton.SetActive(false);
        FoldButton.SetActive(false);
        CallButton.SetActive(false);
        BetButton.SetActive(false);
        RaiseButton.SetActive(false);
    }

    void playerFolded(int position){
        playersInRoundArray[position] = 0;
    }

    void enableAllAreas(){
        for (int i = 0; i < Areas.Length; ++i){
         Areas[i].SetActive(true);
        }
    }

    void startGame(){
        if (money == 0){
            inRound = false;
        }else {
            inRound = true;
        }
        someoneBet = false;
        moneyUpdates = 0;
        betAtPlayer = 20;
        moneyAlreadyBet = 0;
        totalBet = 20;
        playersInRound = numPlayers;
        callsInARow = 0;
        checksInARow = 0;
        round = 0;
        dealer += 1;
        pot = 30;
        JSONinput = "https://api.pokerapi.dev/v1/winner/texas_holdem?";
        JSONoutput = "";
        NickInput = "http://f680915cbcaa.ngrok.io/q/INSERT INTO public.\"Games\"(board, \"player cards\", \"preflop actions\", \"flop actions\", \"turn actions\", \"player names\", winnings, pots, \"time\") VALUES (\'{";
        playersInRoundArray.Clear();
        for (int i = 0; i < numPlayers; ++i){
            if(inRound){
                playersInRoundArray.Add(1);
            }else {
                playersInRoundArray.Add(0);
            }
        }
        for(int i = 0; i < CardsInstan.Count; ++i){
            Destroy(CardsInstan[i]);
        }
        enableAllAreas();
    }

    void startRound(){
        moneyUpdates = 0;
        betAtPlayer = 0;
        moneyAlreadyBet = 0;
        totalBet = 0;
        callsInARow = 0;
        checksInARow = 0;
        round += 1;
        PhotonNetwork.RaiseEvent(UPDATE_HOST_MONEY, null, raiseEventOptions, SendOptions.SendReliable);
    }

    void waitWinnerInRound(){
        if (inRound){
            winner();
        }
    }

    IEnumerator waitBeforeStartGame(){
        yield return new WaitForSeconds(10);
        PhotonNetwork.RaiseEvent(START_POKER_GAME, null, raiseEventOptions, SendOptions.SendReliable);
    }

    IEnumerator startPokerGame(){
        PhotonNetwork.RaiseEvent(START_GAME, null, RaiseEventOptions.Default, SendOptions.SendReliable);
        startGame();
        makeDeck();
        Cards.Clear();
        cardNumbersList.Clear();
        for(int i = 0; i < numPlayers * 2 + 5; ++i){
            Cards.Add(getRandomCard());
        }
        getCards();
        int[] temp = cardNumbersList.ToArray();
        object[] data = new object[] {temp};
        PhotonNetwork.RaiseEvent(GET_CARDS, data, RaiseEventOptions.Default, SendOptions.SendReliable);
        data = new object[] {(dealer + 1) % numPlayers};
        PhotonNetwork.RaiseEvent(SMALL_BLIND, data, raiseEventOptions, SendOptions.SendReliable);
        data = new object[] {(dealer + 2) % numPlayers};
        PhotonNetwork.RaiseEvent(BIG_BLIND, data, raiseEventOptions, SendOptions.SendReliable);
        yield return new WaitForSeconds(4);
        data = new object[] {(dealer + 3) % numPlayers, false};
        PhotonNetwork.RaiseEvent(ACTIVATE_TURN, data, raiseEventOptions, SendOptions.SendReliable);
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError(pages[page] + ": Error");
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    JSONoutput = webRequest.downloadHandler.text;
                    Debug.Log("API Output: " + webRequest.downloadHandler.text);
                    break;
            }
        }
        Debug.Log(JSONoutput);
        string[] thing = JSONoutput.Split('\"');
        string[] holder;
        List <string> winningCards  = new List<string>();
        for (int i = 0; i < thing.Length; ++i){
            if (thing[i] == "cards"){
                holder = thing[i + 2].Split(',');
                winningCards.Add(holder[0]);
            }else if(thing[i] == "players"){
                break;
            }
        }
        for (int i = 0; i < winningCards.Count; i += 2){
            for (int j = 0; j < Cards.Count; ++j){
                if(winningCards[i] == Cards[j].name){
                    int payOut = pot / winningCards.Count;
                    object[] data = new object[] {(j + 1) /2, payOut};
                    PhotonNetwork.RaiseEvent(ADD_MONEY_TO_PLAYER, data, raiseEventOptions, SendOptions.SendReliable);
                }
            }
        }
        PhotonNetwork.RaiseEvent(UPDATE_HOST_MONEY, null, raiseEventOptions, SendOptions.SendReliable);
        PhotonNetwork.RaiseEvent(SHOW_ALL_CARDS, null, raiseEventOptions, SendOptions.SendReliable);
    }

    void Awake(){
        Instance = this;
    }

    void Start()
    {
        makeDeck();
        dealer = -1;
        money = 1000;
        numPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        for(int j = 0; j < numPlayers; ++j){
            allMoney.Add(1000);
        }
        int i = 0;
        foreach (Player p in PhotonNetwork.PlayerList){
            if (p.NickName == PhotonNetwork.NickName){
                playerPosition = i;
            }
            i ++;
        }
        for (i = 0; i < numPlayers; ++i){
            AllNames[i].text = PhotonNetwork.PlayerList[(i + playerPosition) % numPlayers].NickName;
            AllMoney[i].text = "$" + allMoney[i].ToString();
        }

        string test = "2D";
        Debug.Log(test.ToLower());

        if(PhotonNetwork.IsMasterClient){
            StartCoroutine(startPokerGame());
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)){
            PhotonChatManager.Instance.send();
        }
    }
}
