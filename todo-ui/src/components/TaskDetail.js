import React from 'react';

export default function TaskDetail({ task, onBack }) {
  return (
    <div className="container">
      <div className="form-card">
        <button className="back-btn" onClick={onBack}>← Listeye Dön</button>
        <div style={{ marginTop: '20px' }}>
          <h1 style={{ color: '#2d3748', marginBottom: '10px' }}>{task.title}</h1>
          <div style={{ marginBottom: '20px' }}>
            <strong>Durum:</strong> {task.state === 0 ? "Yapılacak" : task.state === 1 ? "Yapılıyor" : "Bitti"}
          </div>
          <div className="detail-content" style={{ backgroundColor: '#f7fafc', padding: '20px', borderRadius: '8px' }}>
            <p style={{ color: '#4a5568', lineHeight: '1.6' }}>{task.content || "Bu görev için açıklama bulunmuyor."}</p>
          </div>
          <p style={{ marginTop: '20px', fontSize: '13px', color: '#a0aec0' }}>
            <strong>Oluşturan:</strong> {task.username || "Sistem"}
          </p>
        </div>
      </div>
    </div>
  );
}