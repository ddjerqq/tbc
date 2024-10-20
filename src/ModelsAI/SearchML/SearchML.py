import pandas as pd
import difflib

prediction_data = pd.read_csv("dataset.csv")


class SearchML:
    def __init__(self):
        pass

    def prefix_predict(self, input_prefix, top_n=5):
        df = prediction_data
        df['prefix'] = df['actual_word'].str.slice(0, len(input_prefix))
        suggestions = df.loc[df['prefix'] == input_prefix]
        sorted_suggestions = suggestions.sort_values(by='usage_value', ascending=False).head(top_n)
        return sorted_suggestions['actual_word'].tolist()

    def misspelled_predict(self, misspelled_word, word_list, cutoff=0.6):
        closest_matches = difflib.get_close_matches(misspelled_word, word_list, n=5, cutoff=cutoff)
        print(closest_matches)
        return closest_matches

    def search(self, input_query):
        predicted = self.prefix_predict(input_query)
        response = []
        if not predicted == []:
            final_predictions = predicted
        else:
            final_predictions = self.misspelled_predict(input_query, prediction_data['actual_word'].tolist())
        for word in final_predictions:
            if word not in response:
                response.append(word)
        return response

