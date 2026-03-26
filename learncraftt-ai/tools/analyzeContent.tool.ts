import { z } from "genkit";
import { ai } from "../genkit";

// geminiyi cagirir. tool .net api'ye goder. .net gercek analizi yapar. sonucu gemini'ye geri verir
export const analyzeContentTool = ai.defineTool(
    {
        name: "analyze_content",
        description: "Analyze content via LearnCraftt .NET backend",
        inputSchema: z.object({
            text: z.string().min(20),
        }),
        outputSchema: z.object({
            summary: z.string(),
            tags: z.array(z.string()),
        }),
    },
    async ({ text }) => {
        const response = await fetch("http://localhost:5048/api/AI/analyze", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({text }),
        });

        if (!response.ok) {
            throw new Error("LearnCraftt backend analysis failed");
        }

        return await response.json();
    }
);
