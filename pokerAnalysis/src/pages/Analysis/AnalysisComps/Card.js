export default function Card(props){
    let card=props.reveal ? props.card : "back";
    return(
        <img style={{width: "6em", padding: "5px"}}src={'../../images/cards/'+card+'.png'}/>
    )
}