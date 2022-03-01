import firebase from "firebase/app";
import "firebase/auth";
import "firebase/firestore"

var firebaseConfig = {
  apiKey: "AIzaSyAunejFdEr15WixSvuwAaFsXBs7fhUMkZA",
  authDomain: "poker-analysis-ae3b8.firebaseapp.com",
  projectId: "poker-analysis-ae3b8",
  storageBucket: "poker-analysis-ae3b8.appspot.com",
  messagingSenderId: "317669383411",
  appId: "1:317669383411:web:fa75a30aad58324ac2eb86",
  measurementId: "G-9BDE5JHZ5K",
};
// Initialize Firebase
const app = firebase.initializeApp(firebaseConfig);

export const auth = app.auth();
export const firestore = app.firestore();
export default app;
