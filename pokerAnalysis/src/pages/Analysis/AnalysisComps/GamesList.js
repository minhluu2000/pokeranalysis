import { useState, useEffect } from 'react';
import {Link} from 'react-router-dom';

export default function GamesList() {
    const [queryRes, setQueryRes] = useState("none");
    const [name, setName] = useState("");
    const months=["January","February","March","April","May","June","July","August","September","October","November","December",];

    async function fetchData(query) {
        const response = await fetch("http://f680915cbcaa.ngrok.io", { headers: { 'Content-Type': 'application/json' }, 
        method: 'POST', body: JSON.stringify({ query: query })})
            .then(res=>res.text())
            .then(res=> {console.log(res)
                setQueryRes(JSON.parse(res)["vals"])
            })
            .catch(err => console.log(err));
    }
    useEffect(()=>{
        fetchData('SELECT "id", "player cards", "player names", "winnings", "time" FROM public."Games" WHERE "player names" LIKE \'%'+name+'%\' ORDER BY "time" DESC LIMIT 20');
        
    },   
    [name]);

    let list=queryRes !== "none" && <table className="table">
    <thead className="thead-dark">
        <tr>
            <th>
                Player Names
            </th>
            <th>
                Date
            </th>
            <th>
                Winner Names
            </th>
            <th>
                Players Cards
            </th>
        </tr>
    </thead>


    <tbody>
    

   {queryRes.map((entry)=>
       <tr>
           <td>
           {entry["player names"].split(",").map((entry)=>
           <div>{entry}<br></br></div>)}  
               
           </td>
           <td>
           {months[parseInt(entry["time"].substr(4,6))]+" "+entry["time"].substr(0,4)}
           </td>
           <td>
                {Array.from(Array(entry["winnings"].split(",").length).keys()).map((i)=>
                    <div>
                        {parseInt(entry["winnings"].split(",")[i])>0 && entry["player names"].split(",")[i]+" "}
                        </div>
                )    
                }
           </td>
           <td>
           {entry["player cards"].split(";").map((cardset)=>
           <div>
           {cardset.split(",").map((card)=>
                <img style={{width: "2em"}}src={'../../images/cards/'+card+'.png'}/>
           )}
           <br>
           </br>
           </div>
           )
        }
           </td>
           <td>
           <Link to={{pathname: "analysis/game", state:{"id": entry["id"]}}}>
              <button className="btn btn-primary">
               View Game
               </button>
               </Link>
           </td>
       </tr>
   )}
   </tbody>
</table>

    return (<div>
        <br></br>
        <div className="row">
        <h5 className="col-sm-3">Search for a name</h5>
        <input className="col-sm-2" type="text" value={name} onChange={(event)=>{
            setName(event.target.value);
        }
        }/>
        </div>
    <br></br>
    <br></br>
         {list}
    </div>
    )

}