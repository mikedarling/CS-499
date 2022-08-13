from pymongo import MongoClient
from bson.objectid import ObjectId

class AnimalShelter(object):
    """ CRUD operations for Animal collection in MongoDB """

    def __init__(self, username, password):
        # Initializing the MongoClient. This helps to 
        # access the MongoDB databases and collections.
        database = "AAC"
        # Updated connection string to include database.
        client = MongoClient('mongodb://%s:%s@localhost:35672/%s' % (username, password, database));
        self.animals = client[database].animals;

    # Complete this create method to implement the C in CRUD.
    def create(self, data):
        """ 
            Creates a document in the collection.
            
            data : JSON object with document fields and values.
            
            returns : A boolean indicating whether or not the document was successfully added.
        """
        if data is None:
            raise Exception("Nothing to save, because data parameter is empty");
            
        # Since .insert() is deprecated, replaced with insert_one().
        result = self.animals.insert_one(data);
        if result is None:
            return false;
        
        return (isinstance(result.inserted_id, ObjectId));

    # Create method to implement the R in CRUD.
    def read(self, query):
        """
            Returns documents matching the query parameters.
            
            query : JSON object with the fields and expected values to match against.
            
            returns : A cursor containing matching documents.
        """
        if query is None:
            raise Exception("No query parameters provided.");
            
        return self.animals.find(query, { "_id" : False });
    
    # Update method to implement the U in CRUD.
    def update(self, query, data):
        """
            Returns JSON objects representing the updated documents.
            
            query : JSON object with the fields and expected values to match against.
            
            data : JSON object representing the document properties to be updated
                    and their respective values.
            
            returns : If successful, the result of the Mongo operation wrapped in a
                        JSON object. Otherwise, the exception as a string.
        """
        if query is None:
            raise Exception("No query parameters provided.");
        if data is None:
            raise Exception("No document properties specified to be updated."); 
        
        try:
            result = self.animals.update_many(query, { "$set" : data });
            return { "result" : result };
        except Exception as e:
            return str(e);
    
    # Delete method to implement the D in CRUD.
    def delete(self, query):
        """
            Returns a JSON object describing the number of documents deleted.
            
            query : JSON object with the fields and expected values to match against.
            
            returns : If successful, the result of the Mongo operation wrapped in a
                        JSON object. Otherwise, the exception as a string.
        """
        if query is None:
            raise Exception("No query parameters provided.");
        
        try:
            result = self.animals.delete_many(query);
            return { "result" : result };
        except Exception as e:
            return str(e);