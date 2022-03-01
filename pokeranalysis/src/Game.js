export default class Game{
    constructor(id){
        this.id=id;
        this.time=0;
        this.board=[];
        this.player_cards=[];
        this.preflop_actions=[];
        this.flop_actions=[];
        this.river_actions=[];
        this.turn_actions=[];
        this.player_names=[];
        this.winners=[];
        this.player_bankrolls=[];
        this.pots=[];
    }

    constructor(board, player_cards, preflop_actions, flop_actions, river_actions, turn_actions, player_names, winners, player_bankrolls, pots){
        this.id=1000000;
        this.time=0;
        this.board=[];
        this.player_cards=[];
        this.preflop_actions=[];
        this.flop_actions=[];
        this.river_actions=[];
        this.turn_actions=[];
        this.player_names=[];
        this.winners=[];
        this.player_bankrolls=[];
        this.pots=[];
    }
}