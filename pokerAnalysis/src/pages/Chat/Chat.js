import React, { useRef, useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { Card } from "react-bootstrap";
import { Container } from "react-bootstrap";
import "./Chat.css";

import { useAuth } from "../../contexts/AuthContext";
import { firestore } from "../../firebase";
import firebase from "firebase/app";

import { useCollectionData } from "react-firebase-hooks/firestore";

const SpeechRecognition =
  window.SpeechRecognition || window.webkitSpeechRecognition;
const mic = new SpeechRecognition();

mic.continuous = true;
mic.interimResults = true;
mic.lang = "en-US";

function Chat() {
  const { currentUser } = useAuth();

  const dummy = useRef();
  const messagesRef = firestore.collection("messages");
  const query = messagesRef.orderBy("createdAt").limit(25);

  const [messages] = useCollectionData(query, { idField: "id" });

  const [isListening, setIsListening] = useState(false);
  const [formValue, setFormValue] = useState("");

  useEffect(() => {
    handleListen();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isListening]);

  const sendMessage = async (e) => {
    e.preventDefault();

    const { uid, photoURL, email } = currentUser;

    await messagesRef.add({
      text: formValue,
      createdAt: firebase.firestore.FieldValue.serverTimestamp(),
      uid,
      photoURL,
      email,
    });

    setFormValue("");
    dummy.current.scrollIntoView({ behavior: "smooth" });
  };

  const handleListen = () => {
    if (isListening) {
      mic.start();
      mic.onend = () => {
        console.log("continue..");
        mic.start();
      };
    } else {
      mic.stop();
      mic.onend = () => {
        console.log("Stopped Mic on Click");
      };
    }
    mic.onstart = () => {
      console.log("Mics on");
    };

    mic.onresult = (event) => {
      const transcript = Array.from(event.results)
        .map((result) => result[0])
        .map((result) => result.transcript)
        .join("");
      console.log(transcript);
      setFormValue(transcript);
      mic.onerror = (event) => {
        console.log(event.error);
      };
    };
  };

  return (
    <>
      <Container
        className="d-flex align-items-center justify-content-center"
        style={{ minHeight: "100vh" }}
      >
        <div className="w-100" style={{ maxWidth: "1000px" }}>
          <Card>
            <Card.Body>
              <h2 className="text-center mb-4">Global Chat</h2>
              <main className="chat-main">
                {messages &&
                  messages.map((msg) => (
                    <ChatMessage
                      key={msg.id}
                      message={msg}
                      currentUser={currentUser}
                    />
                  ))}

                <span ref={dummy}></span>
              </main>

              <center>
                <form className="chat-form" onSubmit={sendMessage}>
                  <input
                    value={formValue}
                    onChange={(e) => setFormValue(e.target.value)}
                    placeholder="Enter message..."
                  />

                  <button
                    className="chat-button"
                    type="submit"
                    disabled={!formValue}
                    onClick={() => {
                      if (isListening)
                        setIsListening((prevState) => !prevState);
                    }}
                  >
                    <i class="far fa-comment-dots"></i>
                  </button>

                  <button
                    className="stt-button"
                    type="button"
                    onClick={() => {
                      setIsListening((prevState) => !prevState);
                    }}
                  >
                    <i class="fas fa-microphone"></i>
                  </button>
                </form>
                <Link to="menu" className="btn btn-primary mt-3">
                  Return to user menu
                </Link>
              </center>
            </Card.Body>
          </Card>
        </div>
      </Container>
    </>
  );
}

function ChatMessage(props) {
  const { text, uid, email } = props.message;

  const messageClass = uid === props.currentUser.uid ? "sent" : "received";

  return (
    <>
      <div className={`message ${messageClass}`}>
        <p>{email}</p>
        <p>:</p>
        <p>{text}</p>
      </div>
    </>
  );
}

export default Chat;
