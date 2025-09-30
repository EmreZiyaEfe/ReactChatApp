import React, {useEffect, useState, useRef} from 'react'

function Chat() {
    //gönderilen mesajları tutar
    const [messages, setMessages] = useState([]);

    //kullanıcı yazdıgı mesajı tutar
    const [input, setInput] = useState('');

    //gönder butonuna basıldıgında çalışan fonk.
    const handleSend = async () => {

        //input boşsa birşey yapma
        if (input.trim() === '') 
            return;
        

        try {
            //Api post istegi
            const response = await fetch('https://localhost:7226/api/Messages/CreateMessage', {
                method: 'POST',
                headers: {
                    'Content-Type' : 'application/json'
                },
                body: JSON.stringify({
                    text: input,
                    userId: 1

                }),
            });

            if(!response.ok) {
                throw new Error("Mesaj gönderilemedi");
            }

            const data = await response.json();

            //Mesajı localstate ekle
            setMessages([...messages, data]);

            //inputu temizle
            setInput('');

        } catch (error) {
            console.log(error);
            alert('Mesaj gönderileriken hata oluştu');
        }
    };

    useEffect(() => {
        const fetchMessages = async () => {
            try {
                const userId = 1;
                const response = await fetch(`https://localhost:7226/api/Messages/GetMessages/${userId}`);
                const data = await response.json();
                const processed = data.map(msg => ({
                ...msg,
                sentimentLabel: msg.sentimentLabel || "Unknown"
            }));
            setMessages(processed);
            }catch(error) {
                console.log('Mesajlar yüklenirken hata oluştu', error);
            }
        };

        fetchMessages();
    }, []); //boş arrray - bir kez çalışır

    const messageEndRef = useRef(null);
    
    useEffect(() => {
        messageEndRef.current?.scrollIntoView({behavior: "smooth"});
    }, [messages]);

    return (
        <div style={{padding: '20px', maxWidth:'500px', margin:'0 auto'}}>
            <h2>Chat App</h2>
            
            {/* Mesajların gösterilecegi kutu */}

            <div style={{border:'1px solid #ccc', padding:'10px', minHeight:'200px', overflowY:'auto', display:'flex', flexDirection:'column', gap:'5px'}}>
                {messages.map((msg, index) => (
                    //Her mesajı div içinde göster
                    <div key={index} style={{
                        alignSelf: msg.userId === 1 ? 'flex-end' : 'flex-start', //kend, mesaj sagda
                        backgroundColor: msg.userId === 1 ? '#dcf8c6' : '#fff', //kendi mesaj rengi
                        padding: '8px 12px',
                        borderRadius: '15px',
                        maxWidth:  '70%',
                        boxShadow: '0px 1px 2px rgba(0,0,0,0.2)'
                    }}>
                        {/* Kullanıcı adı */}
                        <div style={{fontSize:'12px', color:'#555', marginBottom: '3px'}}>
                            {msg.nickName || 'Anonim'}

                        </div>
                        {/* Mesaj */}
                        <div>
                            {msg.text}
                            <span style={{marginLeft: '8px', color: 'blue'}}>
                                [{msg.sentimentLabel || "Unknown"}]
                            </span>
                        </div>
                    </div>
                ))}
                <div ref={messageEndRef}/> {/*En alt ref*/ }
            </div>

            <div style={{display:'flex', marginTop:'10px'}}>
                <input 
                    type='text' 
                    value={input} //input stati baglıyorm
                    onChange={(e) => setInput(e.target.value)} //yazdıkca state guncellenir
                    placeholder='Type a messsage...'
                    style={{
                        flex: 1,
                        padding: '10px 15px',
                        borderRadius: '20px',
                        outline: 'none',
                        fontSize: '14px'
                    }}
                    onKeyDown={(e) => {
                        if(e.key === 'Enter') handleSend(); //Enter ile gonder
                    }}
                />
                {/* Gönder butonu */}
                <button 
                onClick={handleSend} 
                style={{
                    marginLeft: '8px',
                    padding: '5px 10px',
                    backgroundColor: '#007bff',
                    color: '#fff',
                    border: 'none',
                    borderRadius: '20px',
                    cursor: 'pointer',
                    fontWeight: 'bold' 

                    }}>
                    Send
                </button>
            </div>
        </div>
    );
}

//Chat bileşenini dışarı aktar, appjsde kullan
export default Chat;