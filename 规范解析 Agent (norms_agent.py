# norms_agent.py
import openai

class NormsAgent:
    def __init__(self, api_key):
        openai.api_key = api_key

    def parse_norms(self, norm_text):
        response = openai.Completion.create(
            model="text-davinci-003",
            prompt=norm_text,
            max_tokens=1000
        )
        return response.choices[0].text.strip()

if __name__ == "__main__":
    agent = NormsAgent(api_key="your_openai_api_key")
    norm_text = "规范内容：风管需要保持一定的坡度，水管必须具备最低的净高2.2m"
    resolved_norms = agent.parse_norms(norm_text)
    print(resolved_norms)