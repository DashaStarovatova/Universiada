// src/components/GradientArrows.tsx
const arrows = [
  { from: "#783baa", to: "#9e9fdb" },
  { from: "#9e9fdb", to: "#8fff8f" },
  { from: "#93d6b1", to: "#93d6b1" },
  ];
  
  export function GradientArrows() {
    return (
      <div className="flex items-center gap-0.5">
        {arrows.map((arrow, i) => (
          <svg key={i} width="20" height="14" viewBox="0 0 20 14" fill="none">
            <defs>
              <linearGradient id={`arrow${i}`} x1="0%" y1="0%" x2="100%" y2="0%">
                <stop offset="0%" stop-color={arrow.from} />
                <stop offset="100%" stop-color={arrow.to} />
              </linearGradient>
            </defs>
            <polygon points="0,0 0,14 14,7" fill={`url(#arrow${i})`} />
          </svg>
        ))}
      </div>
    );
  }