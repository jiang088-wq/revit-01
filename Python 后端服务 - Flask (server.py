# server.py
from flask import Flask, request, jsonify
import json

app = Flask(__name__)

@app.route('/api/optimize', methods=['POST'])
def optimize():
    # 获取请求中的 Revit 数据
    data = request.json
    space_data = data.get('spaceData')

    # 示例：简单返回接收到的数据
    print(f"Received data: {space_data}")

    # 假设返回优化结果
    result = {
        "status": "success",
        "optimized_data": "Optimized pipeline layout with no collisions"
    }
    
    return jsonify(result)

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0', port=5000)