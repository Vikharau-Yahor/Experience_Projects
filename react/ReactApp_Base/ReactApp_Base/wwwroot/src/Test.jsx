import React from 'react';
import ReactDOM from 'react-dom'

class CommentBox extends React.Component {
    render() {
        return (
            <div>
                <div className="commentBox">Hello, world from Reactsdsd! I am a CommentBox.</div>
                <div className="commentBox">Test.</div>
            </div>
        );
    }
}

ReactDOM.render(<CommentBox />, document.getElementById('content'));