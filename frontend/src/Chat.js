import React, { useEffect, useState, useRef } from "react";

function Chat() {
  const [messages, setMessages] = useState([]);
  const [input, setInput] = useState("");

  const API_URL = process.env.REACT_APP_API_URL; // 🔑 Backend URL

  const handleSend = async () => {
    if (input.trim() === "") return;

    try {
      const response = await fetch(`${API_URL}/api/Messages/CreateMessage`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          text: input,
          userId: 1,
        }),
      });

      if (!response.ok) {
        throw new Error("Mesaj gönderilemedi");
      }

      const data = await response.json();
      setMessages((prev) => [...prev, data]); // eski mesajları koru + yeni ekle
      setInput("");
    } catch (error) {
      console.error(error);
      alert("Mesaj gönderilirken hata oluştu");
    }
  };

  useEffect(() => {
    const fetchMessages = async () => {
      try {
        const userId = 1;
        const response = await fetch(`${API_URL}/api/Messages/GetMessages/${userId}`);
        const data = await response.json();
        const processed = data.map((msg) => ({
          ...msg,
          sentimentLabel: msg.sentimentLabel || "Unknown",
        }));
        setMessages(processed);
      } catch (error) {
        console.error("Mesajlar yüklenirken hata oluştu", error);
      }
    };

    fetchMessages();
  }, [API_URL]);

  const messageEndRef = useRef(null);

  useEffect(() => {
    messageEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);

  return (
    <div style={{ padding: "20px", maxWidth: "500px", margin: "0 auto" }}>
      <h2>Chat App</h2>

      <div
        style={{
          border: "1px solid #ccc",
          padding: "10px",
          minHeight: "200px",
          overflowY: "auto",
          display: "flex",
          flexDirection: "column",
          gap: "5px",
        }}
      >
        {messages.map((msg, index) => (
          <div
            key={index}
            style={{
              alignSelf: msg.userId === 1 ? "flex-end" : "flex-start",
              backgroundColor: msg.userId === 1 ? "#dcf8c6" : "#fff",
              padding: "8px 12px",
              borderRadius: "15px",
              maxWidth: "70%",
              boxShadow: "0px 1px 2px rgba(0,0,0,0.2)",
            }}
          >
            <div style={{ fontSize: "12px", color: "#555", marginBottom: "3px" }}>
              {msg.nickName || "Anonim"}
            </div>
            <div>
              {msg.text}
              <span style={{ marginLeft: "8px", color: "blue" }}>
                [{msg.sentimentLabel || "Unknown"}]
              </span>
            </div>
          </div>
        ))}
        <div ref={messageEndRef} />
      </div>

      <div style={{ display: "flex", marginTop: "10px" }}>
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          placeholder="Type a message..."
          style={{
            flex: 1,
            padding: "10px 15px",
            borderRadius: "20px",
            outline: "none",
            fontSize: "14px",
          }}
          onKeyDown={(e) => {
            if (e.key === "Enter") handleSend();
          }}
        />
        <button
          onClick={handleSend}
          style={{
            marginLeft: "8px",
            padding: "5px 10px",
            backgroundColor: "#007bff",
            color: "#fff",
            border: "none",
            borderRadius: "20px",
            cursor: "pointer",
            fontWeight: "bold",
          }}
        >
          Send
        </button>
      </div>
    </div>
  );
}

export default Chat;
