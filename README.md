🏥 MediManage — 재고관리 시스템

VB.NET(WinForms) 기반의 의약품·재고관리 시스템입니다.
ODCloud 공공데이터 API를 통해 의약품 정보를 주기적으로 수집하고,
소켓 통신을 통해 클라이언트로 전송합니다.
매일 오전 7시 자동 갱신 기능이 포함되어 있습니다.

🚀 주요 기능

- 재고 현황 관리
  - 탭별 DataGridView 구성 (기준항목 / 입출고 / 처방내역)
- ODCloud API 연동
  - OpenAPI 문서에서 최신 엔드포인트 자동 탐색
  - 모든 페이지 데이터를 JArray로 병합 후 저장/전송
- 자동 스케줄 실행
  - Async/Await 기반으로 매일 07:00에 자동 실행
- 대용량 데이터 전송
  - 최대 40MB 이상 데이터도 안정적으로 전송 (길이 프리픽스 + 청크 방식)
- 버전 관리
  - versionIndex.json 파일로 최신 버전/엔드포인트 저장

⚙️ 기술 스택

Language/UI: Visual Basic .NET (WinForms)
Network: TcpClient / NetworkStream
HTTP: HttpWebRequest / HttpClient
JSON: Newtonsoft.Json (JObject, JArray)
Scheduler: Task.Delay (비동기 대기)

📦 데이터 구조

versionIndex.json
```
{ "version": "/15067462/v1/DrugData" }
```

송신 데이터 예시
```
{
  "endpoint": "/15067462/v1/DrugData",
  "data": [ { ... }, { ... } ]
}
```
🔁 동작 흐름

1. 앱 실행 시 versionIndex.json 확인
2. 없으면 API에서 최신 버전 정보 조회
3. 오전 7시에 자동으로 데이터 수집 시작
4. JSON 생성 후 파일 저장 또는 소켓으로 전송
5. 완료 후 다음날 7시까지 대기
