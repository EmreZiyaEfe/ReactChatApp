import gradio as gr
from transformers import pipeline

# Duygu analizi pipeline
classifier = pipeline("sentiment-analysis")

def analyze_sentiment(message):
    result = classifier(message)[0]  # [{label: ..., score: ...}]
    return {"label": result["label"], "score": float(result["score"])}

# Gradio arayüzü
iface = gr.Interface(
    fn=analyze_sentiment,
    inputs=gr.Textbox(lines=2, placeholder="Mesaj yazın..."),
    outputs=["json"],
    title="Duygu Analizi Servisi"
)

if __name__ == "__main__":
    iface.launch(server_name="0.0.0.0", server_port=7860)






# import os
# from flask import Flask, request, jsonify
# from transformers import pipeline

# app = Flask(__name__)
# sentiment = pipeline("sentiment-analysis")

# @app.route("/predict", methods=["POST"])
# def predict():
#     data = request.get_json() or {}
#     if "message" not in data:
#         return jsonify({"error": "Message field required"}), 400
#     message = data["message"]
#     result = sentiment(message)[0]
#     return jsonify({"label": result['label'], "score": float(result['score'])})

# if __name__ == "__main__":
#     port = int(os.environ.get("PORT", 5000))
#     app.run(host="0.0.0.0", port=port)




# from flask import Flask, request, jsonify
# from transformers import pipeline

# app = Flask(__name__)

# # Hugging Face pipeline
# sentiment = pipeline("sentiment-analysis")

# @app.route("/predict", methods=["POST"])
# def predict():
#     data = request.get_json()
#     if "message" not in data:
#         return jsonify({"error": "Message field required"}), 400
    
#     message = data["message"]
#     result = sentiment(message)[0]
#     return jsonify({
#         "label": result['label'],
#         "score": float(result['score'])
#     })

# if __name__ == "__main__":
#     app.run(host="0.0.0.0", port=5000)
