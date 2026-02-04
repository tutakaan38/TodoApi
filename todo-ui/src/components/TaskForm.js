export default function TaskForm({ mode, taskData, setTaskData, onSave, onCancel }) {
  return (
    <div className="container">
      <div className="form-card">
        <button className="back-btn" onClick={onCancel}>← Vazgeç</button>
        <h2>{mode === 'edit' ? 'Görevi Düzenle' : 'Yeni Görev'}</h2>

        <input
          value={taskData.title}
          onChange={(e) => setTaskData({ ...taskData, title: e.target.value })}
          placeholder="Başlık"
        />
        <textarea
          value={taskData.content || taskData.description}
          onChange={(e) => setTaskData({ ...taskData, content: e.target.value, description: e.target.value })}
          placeholder="Detay..."
          rows={5}
        />

        {mode === 'edit' && (
          <select value={taskData.state} onChange={(e) => setTaskData({ ...taskData, state: e.target.value })}>
            <option value={0}>Yapılacak</option>
            <option value={1}>Yapılıyor</option>
            <option value={2}>Bitti</option>
          </select>
        )}

        <button className="add-btn" onClick={onSave} style={{ width: '100%', marginTop: '10px' }}>
          {mode === 'edit' ? 'Güncelle' : 'Kaydet'}
        </button>
      </div>
    </div>
  );
}