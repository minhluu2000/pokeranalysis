import { useState, useEffect } from 'react';
import Card from './Card';
import { CardGroup, OddsCalculator } from 'poker-odds-calculator';
import { Navbar, Nav } from "react-bootstrap";
export default function AnalyzeGame(props) {
    const [queryRes, setQueryRes] = useState("none");
    const [stage, setStage] = useState(0);
    const [actionNum, setActionNum] = useState(0);
    const [playerNum, setPlayerNum] = useState(0);
    const [currAction, setCurrAction] = useState("");
    const [equities, setEquities] = useState(null);
    const [numTrials, setTrials] = useState(1);
    const [potOdds, setPotOdds] = useState(null);

    const actionsLookup = { 'c': 'calls', 'k': 'checks', 'B': 'blind bets', 'A': 'goes all in', 'f': 'folds', '-': "can't bet", 'b': 'bets', 'r': 'raises' }
    const actionLookup = { 'c': 'call', 'k': 'check', 'B': 'blind bet', 'A': 'go all in', 'f': 'fold', '-': "can't bet", 'b': 'bet', 'r': 'raise' }
    const stageLookup = ["Preflop", "Flop", "River", "Turn"];
    const allCards = ['As', '2s', '3s', '4s', '5s', '6s', '7s', '8s', '9s', 'Ts', 'Js', 'Qs', 'Ks', 'Ah', '2h', '3h', '4h', '5h', '6h', '7h', '8h', '9h', 'Th', 'Jh', 'Qh', 'Kh', 'Ad', '2d', '3d', '4d', '5d', '6d', '7d', '8d', '9d', 'Td', 'Jd', 'Qd', 'Kd', 'Ac', '2c', '3c', '4c', '5c', '6c', '7c', '8c', '9c', 'Tc', 'Jc', 'Qc', 'Kc']

    async function fetchData(query) {
        const response = await fetch("http://f680915cbcaa.ngrok.io", { headers: { 'Content-Type': 'application/json' }, method: 'POST', body: JSON.stringify({ query: query }) })
            .then(res => res.text())
            .then(res => setQueryRes(JSON.parse(res)["vals"]))
            .catch(err => console.log(err));
    }

    useEffect(() => {
        fetchData('SELECT * FROM public."Games" WHERE id = ' + props.location.state.id + ";");
    },
        []);

    let board;
    let playerInfo = [];
    let players;
    let playerCards;
    let pots;
    let actions;
    if (queryRes != "none") {
        board = [queryRes[0]["board"].substring(1, 3), queryRes[0]["board"].substring(6, 8), queryRes[0]["board"].substring(11, 13), queryRes[0]["board"].substring(16, 18), queryRes[0]["board"].substring(21, 23)]
        players = queryRes[0]["player names"].split(",");
        playerCards = queryRes[0]["player cards"].split(";");
        pots = queryRes[0]["pots"].split(",");
        actions = [queryRes[0]["preflop actions"].substring(0, queryRes[0]["preflop actions"].length - 1).split("],"), queryRes[0]["flop actions"].substring(0, queryRes[0]["flop actions"].length - 1).split("],"), queryRes[0]["river actions"].substring(0, queryRes[0]["river actions"].length - 1).split("],"), queryRes[0]["turn actions"].substring(0, queryRes[0]["turn actions"].length - 1).split("],")];
        for (let i = 0; i < playerCards.length; i++) {
            playerCards[i] = playerCards[i].split(",");
        }
        for (let i = 0; i < actions.length; i++) {
            for (let j = 0; j < actions[i].length; j++) {
                actions[i][j] = actions[i][j].substring(1, actions[i][j].length).split(", ");
            }
        }
        for (let i = 0; i < players.length; i++) {
            playerInfo.push({});
            playerInfo[i]["name"] = players[i];
            playerInfo[i]["cards"] = playerCards[i];
            let playeractions = [];
            for (let round of actions) {
                playeractions.push(round[i]);
            }
            playerInfo[i]["actions"] = playeractions;
        }
    }

    useEffect(() => {
        calculateProbability(playerNum, stage);
    }, [playerNum, stage, queryRes]);
    async function calculateProbability(playernum, turn) {
        setEquities(null);
        if (queryRes !== "none") {
            let b = turn === 3 ? board : turn === 2 ? [board[0], board[1], board[2], board[3]] : turn === 1 ? [board[0], board[1], board[2]] : [];
            let cards = playerInfo[playernum]["cards"];

            let bstring = "";
            let allPlayerCards=[];
            for (let i = 0; i < b.length; i++) {
                bstring += b[i];
                allCards.splice(allCards.indexOf(b[i]), 1);
            }
            for(let i=0; i<playerInfo.length; i++){
            let cstring = "";
            let cards = playerInfo[i]["cards"];
            for (let i = 0; i < cards.length; i++) {
                cstring += cards[i];
            }
            allPlayerCards.push(CardGroup.fromString(cstring))
        }
            
            b = CardGroup.fromString(bstring);


            const result = JSON.parse(JSON.stringify(OddsCalculator.calculate(allPlayerCards, b)));
            console.log(result);
            setEquities(result.equities);
            let potOdds;
            if (stage > 0) {
                setPotOdds(Math.trunc(10000*((pots[stage]-pots[stage-1])/playerInfo.length)*1.0/pots[stage])/100.0+"%");
            }
            else {
                setPotOdds(Math.trunc(10000*(1/pots[stage] * ((pots[stage]) / playerInfo.length)))/100.0+"%");
            }
            //console.log(potOdds);
        }
    }

    function recommendedAction(playernum){
        let actions=[];
        if(queryRes !=="none" && equities && potOdds){
            
            //return parseInt(potOdds.split("%")[0]);
            let oddsDiff=100*parseInt(equities[playernum].bestHandCount)*1.0/(parseInt(equities[playernum].possibleHandsCount)-parseInt(equities[playernum].tieHandCount))-parseInt(potOdds.split("%")[0]);
            if(stage===0){
                actions.push("B");
            }
            if(oddsDiff>10){
                actions.push("b");
                actions.push("r");
            }
            else if(oddsDiff>-5){
                actions.push("k");
                actions.push("c");
            }
            else{
                actions.push("k");
                actions.push("f");
            }
        }
        return actions;
        }

    function next() {
        if (queryRes !== "none") {
            if(stage<3)
                setStage(stage+1);
        }
    }

    function previous() {
        if (queryRes !== "none") {
            if(stage>0)
                setStage(stage-1);
        }
    }
    return (

        <div style={{position:"relative"}}>
            {queryRes != "none" && stage < 4 && stage > -1 &&
                <div style={{padding: "15px"}}>
                    <div className="row">
                    <div className="col-sm-3"><button className="btn btn-success" onClick={previous}>&lt; Previous</button></div>
                    <div className="col-sm-6">
                        <h3>{stageLookup[stage]}&nbsp;pot odds:&nbsp;{potOdds}</h3>
                        {/*Player {players[playerNum] + " " + actionsLookup[playerInfo[playerNum]["actions"][stage][actionNum].substring(1, 2)]*/}
                    </div>
                    <div className="col-sm-3"><button className="btn btn-success" onClick={next}>Next &gt;</button></div>
                    
                    </div>
                    <div style={{width: "70vw", backgroundImage: `url("../../images/felt.jpg")`, backgroundRepeat: "no-repeat", backgroundSize: "cover"}}>
                    <br></br>
                    <div className="row">
                    <div className="col-sm-3"></div>
                    <Card reveal={stage > 0} card={board[0]}></Card>
                    <Card reveal={stage > 0} card={board[1]}></Card>
                    <Card reveal={stage > 0} card={board[2]}></Card>
                    <Card reveal={stage > 1} card={board[3]}></Card>
                    <Card reveal={stage > 2} card={board[4]}></Card>
                    </div>
                    
                    <h2 style={{textAlign: "center", color: "white"}}>Pot size: {pots[stage]}</h2>

                    <div className="row">
                    <div className="col-sm-3"></div>
                    <span style={{height: "10px"}}></span>
                    {Array.from(Array(playerInfo.length).keys()).map((playernum) =>
                        <div className="col-sm-2">
                            <Card reveal={true} card={playerInfo[playernum]["cards"][0]}></Card>
                            <span style={{height: "10px"}}></span>
                            <Card reveal={true} card={playerInfo[playernum]["cards"][1]}></Card>
                            <span style={{height: "10px"}}></span>
                        </div>
                    )}
                     <br></br>
                    </div>
                    </div>
                    <br></br>
                    <div className="row">
                    <h3 className="col-sm-3"></h3>
                    {Array.from(Array(playerInfo.length).keys()).map((playernum) =>
                        <h2 className="col-sm-2" style={{textDecoration: "underline"}}>
                            {playerInfo[playernum]["name"]}
                        </h2>
                    )}
                    </div>
                    <div className="row">
                    <h3 className="col-sm-3">Actions: </h3>
                    {Array.from(Array(playerInfo.length).keys()).map((playernum) =>
                    <div className="col-sm-2">
                        {playerInfo[playernum]["actions"][stage].map((act)=>
                            <h5>
                                {actionsLookup[act.substring(1, 2)]}</h5>
                        )}
                     </div>
                     )}
                     </div>
                     <br></br>
                    <div className="row">
                    <h3 className="col-sm-3">Recommended Actions: </h3>
                    {Array.from(Array(playerInfo.length).keys()).map((playernum) =>
                    <div className="col-sm-2">
                        {recommendedAction(playernum).map((act)=>
                            <h5>{actionLookup[act]}</h5>
                        )}
                     </div>
                     )}
                     </div>
                    <br></br>
                    <div className="row">
                    <h3 className="col-sm-3">Equities: </h3>
                    {Array.from(Array(playerInfo.length).keys()).map((playernum) =>
                    <div className="col-sm-2">
                        <h5>{equities&&Math.trunc(10000*parseInt(equities[playernum].bestHandCount)*1.0/(parseInt(equities[playernum].possibleHandsCount)-parseInt(equities[playernum].tieHandCount)))/100.0+"%"}</h5>
                     </div>
                     )}
                     </div>
                </div>
            }
            <div style={{height:"10em"}}></div>
        </div>
    )
}