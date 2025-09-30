from flask import Flask, request, jsonify
from transformers import pipeline

app = Flask(__name__)

# Hugging Face pipeline
sentiment = pipeline("sentiment-analysis")

@app.route("/predict", methods=["POST"])
def predict():
    data = request.get_json()
    if "message" not in data:
        return jsonify({"error": "Message field required"}), 400
    
    message = data["message"]
    result = sentiment(message)[0]
    return jsonify({
        "label": result['label'],
        "score": float(result['score'])
    })

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
