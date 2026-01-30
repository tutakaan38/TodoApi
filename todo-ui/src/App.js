import { useState, useEffect } from 'react';
import axios from 'axios';

const API_URL = "https://localhost:7233/api/tasks";

function App() {
  const [tasks, setTasks] = useState([]);
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [selectedTask, setSelectedTask] = useState(null); // Detay görünümü için

  useEffect(() => { fetchTasks(); }, []);

  const fetchTasks = async () => {
    const response = await axios.get(API_URL);
    setTasks(response.data);
  };

  const addTask = async () => {
    if (!title || !content) return alert("Lütfen iki alanı da doldurun!");
    // Backend'deki TaskCreateDto: Title ve Content bekliyor
    await axios.post(API_URL, { Title: title, Content: content });
    setTitle(""); setContent("");
    fetchTasks();
  };

  // Eğer bir görev seçildiyse "Detay Sayfası"nı göster
  if (selectedTask) {
    return (
      <div className="detail-container">
        <button onClick={() => setSelectedTask(null)}>← Geri Dön</button>
        <h1>{selectedTask.title}</h1>
        <hr />
        <p><strong>İçerik:</strong> {selectedTask.description || selectedTask.content}</p>
        <p><strong>Durum:</strong> {selectedTask.state === 0 ? "Yapılacak" : "Bitti"}</p>
      </div>
    );
  }

  const deleteTask = async (id) => {
    if (window.confirm("Bu görevi silmek istediğinizden emin misiniz?")) {
      try {
        await axios.delete(`${API_URL}/${id}`);
        fetchTasks();
      } catch (error) {
        console.log("Görev silinemedi:" + error);
        alert("Görev silinemedi");
      }
    }
  }

  return (
    <div className="container">
      <h1>Todo App</h1>

      {/* İki Input Alanı */}
      <div className="input-group">
        <input value={title} onChange={(e) => setTitle(e.target.value)} placeholder="Başlık..." />
        <input value={content} onChange={(e) => setContent(e.target.value)} placeholder="İçerik (Description)..." />
        <button onClick={addTask}>Add</button>
      </div>

      <div className='cards'>
        {tasks.map(task => (
          <div key={task.id} className='card-wrapper'>
            {/* display: flex ve justify-content: space-between butonu en sağa iter */}
            <div
              className='card-header'
              style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', width: '100%' }}
              onClick={() => setSelectedTask(task)} // Kartın geneline tıklayınca detaya gitmesi için
            >
              <span>{task.title}</span>

              <button
                className="delete-btn-small"
                onClick={(e) => {
                  e.stopPropagation(); // Detay sayfasının açılmasını engeller
                  deleteTask(task.id);
                }}
              >
                Delete
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default App;